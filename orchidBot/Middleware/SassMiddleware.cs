using Noobot.Core.MessagingPipeline.Middleware;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;

namespace OrchidBot.Middleware
{
    public class SassMiddleware : MiddlewareBase
    {
        private static readonly ILog Log = LogManager.GetLogger<OrchidBotHost>();

        public SassMiddleware(IMiddleware next) : base(next)
        {
            HandlerMappings = new[]
            {
               new HandlerMapping
                {
                    ValidHandles = new []{ "" },
                    Description = "I'm batman and I can say what I want",
                    EvaluatorFunc = SassHandler,
                    ShouldContinueProcessing = true
                },
            };
        }

        private static Random rand = new Random();

        private IEnumerable<ResponseMessage> SassHandler(IncomingMessage message, string matchedHandle)
        {
            var commandsList = new List<string>();
            if (message.ChannelType == ResponseType.Channel)
            {
                if (rand.Next(1000) == 0)
                {
                    yield return message.ReplyToChannel("I'm Batman");
                }
                else if (rand.Next(500) == 0)
                {
                    yield return message.ReplyDirectlyToUser("I'm Batman");
                }
            }
            if ((message.BotIsMentioned && message.RawText.ToLower().Contains("alive") && message.ChannelType == ResponseType.Channel) ||
                (message.RawText.ToLower().Contains("alive") && message.ChannelType == ResponseType.DirectMessage))
            {
                var val = rand.Next(7);
                switch (val)
                {
                    case 0:
                        yield return message.ReplyToChannel("Maybe");
                        break;

                    case 1:
                        yield return message.ReplyToChannel("Sure");
                        break;

                    case 2:
                        yield return message.ReplyToChannel("No");
                        break;

                    case 3:
                        yield return message.ReplyToChannel("Yes");
                        break;

                    case 4:
                        yield return message.ReplyToChannel("Check my next movie");
                        break;

                    default:
                        yield return message.ReplyToChannel("I'm Batman");
                        break;
                }
            }
        }
    }
}