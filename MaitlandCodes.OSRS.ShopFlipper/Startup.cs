using Microsoft.Extensions.DependencyInjection;
using MIFCore.Settings;
using MaitlandCodes.OSRS.GEItemAPI;
using MaitlandCodes.OSRS.ShopFlipper.Database;
using Microsoft.EntityFrameworkCore;
using MaitlandCodes.OSRS.ShopFlipper.Jobs;
using MIFCore.Hangfire;

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

            serviceDescriptors.AddScoped<CatalogueConsumer>();
            serviceDescriptors.AddScoped<ItemConsumer>();
        }

        public void Configure()
        {

        }

        public void PostConfigure(ShopFlipperDbContext dbContext, IRecurringJobFactory recurringJobFactory)
        {
            dbContext.Database.Migrate();

            recurringJobFactory.CreateRecurringJob<CatalogueConsumer>("categories", y => y.EnqueueCategoriesToConsume());
        }
    }
}