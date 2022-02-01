using MIFCore;
using Microsoft.Extensions.Hosting;
using System;
using MIFCore.Http;
using MIFCore.Hangfire.Analytics;

namespace MaitlandCodes.OSRS.ShopFlipper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            IntegrationHost.CreateDefaultBuilder(args)
                .UseAspNetCore()
                .UseAppInsights()
                .UseStartup<Startup>();
    }
}
