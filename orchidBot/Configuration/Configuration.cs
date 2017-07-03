using Noobot.Core.Configuration;
using OrchidBot.Middleware;

namespace OrchidBot.Configuration
{
    public class Configuration : ConfigurationBase
    {
        public Configuration()
        {
            UseMiddleware<InfoMiddleware>();
            UseMiddleware<MantisMiddleware>();
            UseMiddleware<SassMiddleware>();
        }
    }
}