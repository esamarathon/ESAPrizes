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
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builderContext, config) => {
                    IWebHostEnvironment env = builderContext.HostingEnvironment;

                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile(
                        "appsettings.default.json", optional: false);
                    config.AddJsonFile(
                        "appsettings.json", optional: true);
                    config.AddJsonFile(
                        $"appsettings.{env.EnvironmentName}.json", optional: true);
                    config.AddEnvironmentVariables("ESAPRIZES_");
                })
                .UseStartup<Startup>();
    }
}
