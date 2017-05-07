using Common.Logging;
using Noobot.Core;
using Noobot.Core.Configuration;
using Noobot.Core.DependencyResolution;
using Noobot.Core.Logging;
using System;

namespace OrchidBot
{
    public class OrchidBotHost
    {
        private readonly IConfigReader _configReader;
        private INoobotCore _noobotCore;
        private readonly IConfiguration _configuration;
        private static readonly ILog Log = LogManager.GetLogger<OrchidBotHost>();

        public OrchidBotHost(IConfigReader configReader)
        {
            _configReader = configReader;
            _configuration = new OrchidBot.Configuration.Configuration();
        }

        public void Start()
        {
            IContainerFactory containerFactory = new ContainerFactory(_configuration, _configReader, Log);
            INoobotContainer container = containerFactory.CreateContainer();
            _noobotCore = container.GetNoobotCore();

            Log.Info("Connecting...");
            _noobotCore
                .Connect()
                .ContinueWith(task =>
                {
                    if (!task.IsCompleted || task.IsFaulted)
                    {
                        Console.WriteLine($"Error connecting to Slack: {task.Exception}");
                    }
                })
                .Wait();
        }

        public void Stop()
        {
            Log.Info("Disconnecting...");
            _noobotCore?.Disconnect();
        }
    }
}