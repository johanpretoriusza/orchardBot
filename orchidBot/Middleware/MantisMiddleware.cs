using Noobot.Core.MessagingPipeline.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using System.Text.RegularExpressions;
using Common.Logging;
using OrchidBot.MantisConnect;
using System.ServiceModel;

namespace OrchidBot.Middleware
{
    public class MantisMiddleware : MiddlewareBase
    {
        private static readonly ILog Log = LogManager.GetLogger<OrchidBotHost>();

        private class bug_to_search
        {
            public string bugNumber { get; set; }
        }

        public MantisMiddleware(IMiddleware next) : base(next)
        {
            HandlerMappings = new[]
            {
                new HandlerMapping
                {
                    ValidHandles = new []{ "" },
                    Description = "",
                    EvaluatorFunc = PhrasingHandler,
                    ShouldContinueProcessing = false
                },
            };
        }

        private IEnumerable<ResponseMessage> PhrasingHandler(IncomingMessage message, string matchedHandle)
        {
            var listOfBugs = new List<bug_to_search>();

            Regex regex = new Regex(Properties.Settings.Default.MantisBugRegex);
            Match match = regex.Match(message.RawText);
            while (match.Success)
            {
                if (listOfBugs.Where(x => x.bugNumber == match.Value).Count() == 0)
                {
                    listOfBugs.Add(new bug_to_search()
                    {
                        bugNumber = match.Value.Replace('#', '0')
                    });
                }
                match = match.NextMatch();
            }

            if (listOfBugs.Count > 0)
            {
                //see if we can connect?
                OrchidBot.MantisConnect.MantisConnectPortTypeClient mantisConnection = null;
                try
                {
                    BasicHttpBinding binding = new BasicHttpBinding();
                    EndpointAddress address =
                   new EndpointAddress($"{Properties.Settings.Default.MantisUrl}/api/soap/mantisconnect.php");
                    mantisConnection = new MantisConnectPortTypeClient(binding, address);
                }
                catch (Exception e)
                {
                    Log.Error("Could not connect to mantis", e);
                    mantisConnection = null;
                }

                //now we look up the mantis information
                if (mantisConnection != null)
                {
                    string feedback = "";
                    foreach (var item in listOfBugs)
                    {
                        IssueData result = null;
                        try
                        {
                            result = mantisConnection.mc_issue_get(Properties.Settings.Default.MantisUser, Properties.Settings.Default.MantisPassword, item.bugNumber);
                        }
                        catch (Exception e)
                        {
                            Log.Error(e.Message, e);
                        }
                        if (result != null)
                        {
                            if (result.status.name != "Closed")
                                feedback += $"`<{Properties.Settings.Default.MantisUrl}/view.php?id={result.id}|#{result.id.PadLeft(8, '0')} ({result.status.name} - {result.handler.real_name}): {result.summary}>`\n";
                            else
                                feedback += $"`<{Properties.Settings.Default.MantisUrl}/view.php?id={result.id}|#{result.id.PadLeft(8, '0')} ({result.status.name}): {result.summary}>`\n";
                        }
                    }
                    if (feedback.Length != 0)
                    {
                        yield return message.ReplyToChannel(">>>" + feedback.TrimEnd('\n'));
                    }
                }
            }
        }
    }
}