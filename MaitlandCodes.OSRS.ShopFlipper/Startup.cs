using Microsoft.Extensions.DependencyInjection;
using MIFCore.Settings;
using MaitlandCodes.OSRS.GEItemAPI;
using MaitlandCodes.OSRS.ShopFlipper.Database;
using Microsoft.EntityFrameworkCore;

namespace MaitlandCodes.OSRS.ShopFlipper
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddIntegrationSettings<AppConfig>();
            serviceDescriptors.AddGEItemAPI();

            serviceDescriptors.AddDbContext<ShopFlipperDbContext>((svc, opt) =>
            {
                var appConfig = svc.GetRequiredService<AppConfig>();
                opt.UseSqlServer(appConfig.ConnectionString, cfg => cfg.EnableRetryOnFailure());
            });
        }

        public void Configure()
        {

        }
    }
}