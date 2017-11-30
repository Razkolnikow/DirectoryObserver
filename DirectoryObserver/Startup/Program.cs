using System;
using Startup.Configuration;


namespace Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationService();
            config.ConfigureDependencies().Run();
        }
    }
}
