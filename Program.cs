using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ESAPrizes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config => {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile(
                    "appsettings.default.json", optional: true);
                config.AddJsonFile(
                    "appsettings.json", optional: true);
                config.AddEnvironmentVariables("ESAPRIZES_");
            })
                .UseStartup<Startup>();
    }
}
