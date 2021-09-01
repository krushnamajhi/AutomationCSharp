using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class ConfigReader
    {
        public static IConfigurationRoot IConfig;

        public static void _Build(String settings = "appsettings")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"{settings}.json", reloadOnChange: true, optional: false);

            IConfig = builder.Build();
        }
    }
}
