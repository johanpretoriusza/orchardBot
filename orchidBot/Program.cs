using Noobot.Core.Configuration;
using OrchidBot.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace OrchidBot
{
    class Program
    {
        private static readonly IConfigReader Config = new ConfigReader();

        public static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            HostFactory.Run(x =>
            {
                x.Service<OrchidBotHost>(s =>
                {
                    s.ConstructUsing(name => new OrchidBotHost(Config));

                    s.WhenStarted(n =>
                    {
                        n.Start();
                    });

                    s.WhenStopped(n => n.Stop());
                });

                x.RunAsNetworkService();
                x.SetDisplayName("orchidBot");
                x.SetServiceName("orchidBot");
                x.SetDescription("orchidBot is a slackbot that interfaces to MantisBT");
            });
        }
    }
}
