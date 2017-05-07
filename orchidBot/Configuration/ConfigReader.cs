using System;
using System.IO;
using Newtonsoft.Json.Linq;
using Noobot.Core.Configuration;

namespace OrchidBot.Configuration
{
    public class ConfigReader : IConfigReader
    {
        public string SlackApiKey()
        {
            return Properties.Settings.Default.SlackAPIKey;
        }

        public bool HelpEnabled()
        {
            return false;
        }

        public T GetConfigEntry<T>(string entryName)
        {
            //we are not passing any other settings to Noobot
            return default(T);
        }
    }
}