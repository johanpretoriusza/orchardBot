using System;
using Noobot.Core.MessagingPipeline.Middleware;
using Noobot.Core.MessagingPipeline.Request;
using Noobot.Core.MessagingPipeline.Response;
using System.Collections.Generic;
using Common.Logging;
using RestSharp.Extensions;

namespace OrchidBot.Middleware
{
    public class SassMiddleware : MiddlewareBase
    {
        DateTime thisDay = DateTime.Today;
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

        //Random time reponds to a user or channel
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
                    yield return message.ReplyDirectlyToUser("Why so serious?");
                }
            }
            else if (rand.Next(250) == 0)
            {
                yield return message.ReplyDirectlyToUser("http://imgur.com/a/kh8wc");

            }
                //Call the bot to check if its still alive and responding to your messages using alive statements
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
            //Language check in a channel where the bot is active
            if (message.RawText.ToLower().Contains("fok"))
            {
                yield return message.ReplyToChannel("`Hey!!! Watch your language`");
            }
            if (message.RawText.ToLower().Contains("meme me"))
            {
                yield return message.ReplyToChannel("http://imgur.com/a/kh8wc");
            }
            if (message.RawText.ToLower().Contains("fuck"))
            {
                yield return message.ReplyToChannel("`Hey!!! Watch your language`");
            }
            if (message.RawText.ToLower().Contains("fec"))
            {
                yield return message.ReplyToChannel("`Hey!!! no cheating, watch your language`");
            }
            if (message.RawText.ToLower().Contains("#fec"))
            {
                yield return message.ReplyToChannel("`Hey!!! no cheating, watch your language`");

            }
            //Chirping when someone needs help
            if (message.RawText.ToLower().Contains("help"))
            {
                var val_help = rand.Next(20);
                switch (val_help)
                {
                    case 0:
                        yield return message.ReplyToChannel("`Depends on what you need help with, but let me call Alfred instead`");
                        break;

                    case 1:
                        yield return message.ReplyToChannel("`mmmmmmm I assume you think this is google right??`");
                        break;

                    case 2:
                        yield return message.ReplyToChannel("`Madness, as you know, is like gravity, all it takes is a little push.`");
                        break;

                    case 3:
                        yield return message.ReplyToChannel("`Everyone needs this is Gotham!!!`");
                        break;

                    case 4:
                        yield return message.ReplyToChannel("`tada dum ta dum to the rescue`");
                        break;

                    case 5:
                        yield return message.ReplyToChannel("I'm the Dark Night");
                        break;

                    default:
                        break;
                }
            }
            //Chirping when someone said something is broken
            if (message.RawText.ToLower().Contains("broken"))
            {
                var val_broken = rand.Next(15);
                switch (val_broken)
                {
                    case 0:
                        yield return message.ReplyToChannel("Well,Well thinking that shit will always just work? this isn't Wayne Enterprises!!!");
                        break;

                    case 1:
                        yield return message.ReplyToChannel("`Let me know, I can organise you some concrete with that`");
                        break;

                    case 2:
                        yield return message.ReplyToChannel("`Maybe you are just butt ugly?`");
                        break;

                    case 3:
                        yield return message.ReplyToChannel("`What, have you tested it more than once.?`");
                        break;

                    case 4:
                        yield return message.ReplyToChannel("`Ticket Number??`");
                        break;

                    default:
                        break;
                }
            }
            //Bot will greet randomly from time to time, when using Hi in the channel
            if (message.RawText.ToLower().Contains("hi"))
            {
                var val_greetings = rand.Next(15);
                switch (val_greetings)
                {
                    case 0:
                        yield return message.ReplyToChannel("Welcome ");
                        break;

                    case 1:
                        yield return message.ReplyToChannel("`What do you wwaaaannnttt????`");
                        break;

                    case 2:
                        yield return message.ReplyToChannel("Sup Butt ugly :yum:");
                        break;

                    case 3:
                        yield return message.ReplyToChannel(":face_with_rolling_eyes:");
                        break;

                    default:
                        break;
                }
            }
            //Bot will greet randomly from time to time, when using Hi in the channel
            if (message.RawText.ToLower().Contains("sup"))
            {
                var val_greetings = rand.Next(7);
                switch (val_greetings)
                {
                    case 0:
                        yield return message.ReplyToChannel("Yo Yo");
                        break;

                    case 1:
                        yield return message.ReplyToChannel("`Sup m8`");
                        break;

                    case 2:
                        yield return message.ReplyToChannel("Butt ugly in da house :yum:");
                        break;

                    case 3:
                        yield return message.ReplyToChannel(":face_with_rolling_eyes:");
                        break;

                    default:
                        break;
                }
            }
            //Chirping when someone said something is broken
            if (message.RawText.ToLower().Contains("werk nie"))
            {
                var val_broken = rand.Next(7);
                switch (val_broken)
                {
                    case 0:
                        yield return message.ReplyToChannel("Well,Well thinking that shit will always just work? this isn't Wayne Enterprises!!!");
                        break;

                    case 1:
                        yield return message.ReplyToChannel("`Let me know, I can organise you some concrete with that`");
                        break;

                    case 2:
                        yield return message.ReplyToChannel("`Maybe you are just butt ugly?`");
                        break;

                    case 3:
                        yield return message.ReplyToChannel("`What, have you tested it more than once.?`");
                        break;

                    default:
                        break;
                }
            }
            //dont insult the bat!!
            if ((message.BotIsMentioned && message.RawText.ToLower().Contains("retarded") && message.ChannelType == ResponseType.Channel) ||
            (message.RawText.ToLower().Contains("retarded") && message.ChannelType == ResponseType.DirectMessage))
            {
                var val_sassy = rand.Next(7);
                switch (val_sassy)
                {
                    case 0:
                        yield return message.ReplyToChannel("Well well looky look a hero aren't you, IQ equivalent of sea trash");
                        break;

                    case 1:
                        yield return message.ReplyToChannel("Hold that thought let me ask your mom quickly");
                        break;

                    case 2:
                        yield return message.ReplyToChannel("No");
                        break;

                    case 3:
                        yield return message.ReplyToChannel("I was trying to act like you today, so Yes");
                        break;

                    default:
                        yield return message.ReplyToChannel("Just acting like you...");
                        break;
                }
            }

            if ((message.BotIsMentioned && message.RawText.ToLower().Contains("time") && message.ChannelType == ResponseType.Channel) ||
            (message.RawText.ToLower().Contains("time") && message.ChannelType == ResponseType.DirectMessage))
            {
                var var_GetDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                {
                    yield return message.ReplyToChannel("Today is " + var_GetDate);
                }
            }

            if ((message.BotIsMentioned && message.RawText.ToLower().Contains("thank you") && message.ChannelType == ResponseType.Channel) ||
            (message.RawText.ToLower().Contains("thank you") && message.ChannelType == ResponseType.DirectMessage))
            {
                var val_polite = rand.Next(7);
                switch (val_polite)
                {
                    case 0:
                        yield return message.ReplyToChannel("it's a pleasure");
                        break;

                    case 1:
                        yield return message.ReplyToChannel("You're Welcome");
                        break;

                    default:
                        yield return message.ReplyToChannel(":smile:");
                        break;
                }
            }
        }
    }
}

