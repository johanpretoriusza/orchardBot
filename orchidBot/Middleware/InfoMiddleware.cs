using Noobot.Core.MessagingPipeline.Middleware;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrchidBot.Middleware
{
    public class InfoMiddleware : MiddlewareBase
    {
        public InfoMiddleware(IMiddleware next) : base(next)
        {
            HandlerMappings = new[]
            {
                new HandlerMapping
                {
                    ValidHandles = new []{ "about" },
                    Description = "Shows information on @{bot}",
                    EvaluatorFunc = AboutHandler,
                    ShouldContinueProcessing = false
                },
                new HandlerMapping
                {
                    ValidHandles = new []{ "help" },
                    Description = "Lists the all the available commands",
                    EvaluatorFunc = HelpHandler,
                    ShouldContinueProcessing = false
                }
            };
        }

        private IEnumerable<ResponseMessage> AboutHandler(IncomingMessage message, string matchedHandle)
        {
            if (message.ChannelType == ResponseType.Channel)
            {
                yield return message.ReplyToChannel($"{message.Username}, I have sent you some information");
            }
            yield return message.ReplyDirectlyToUser(" >>>orchidBot - Created by Johan Pretorius\nI am here to answer your MantisBT questions\nYou can find my commands with /help");
        }

        private IEnumerable<ResponseMessage> HelpHandler(IncomingMessage message, string matchedHandle)
        {
            var commandsList = new List<string>();

            var builder = new StringBuilder();
            builder.Append(">>>Commands I respond to:\n\n");
            builder.Append($"`regex - {Properties.Settings.Default.MantisBugRegex}`\t- Try and retrieve information on bug number #\n");
            IEnumerable<CommandDescription> supportedCommands = GetSupportedCommands().OrderBy(x => x.Command).Where(x => x.Description != "");

            foreach (CommandDescription commandDescription in supportedCommands)
            {
                if (commandsList.Contains(commandDescription.Command))
                    continue;

                commandsList.Add(commandDescription.Command);
                string description = commandDescription.Description.Replace("@{bot}", "orchidBot").Replace("noobot", "orchidBot");
                builder.Append($"{commandDescription.Command}\t\t- {description}\n");
            }
            if (message.ChannelType == ResponseType.Channel)
            {
                yield return message.ReplyToChannel($"{message.Username}, I have sent you a list of my commands");
            }
            yield return message.ReplyDirectlyToUser(builder.ToString());
        }
    }
}