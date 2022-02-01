using Microsoft.Extensions.DependencyInjection;
using MIFCore.Settings;
using MaitlandCodes.OSRS.GEItemAPI;

namespace MaitlandCodes.OSRS.ShopFlipper
{
    internal class Startup
    {
        public void ConfigureServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddIntegrationSettings<AppConfig>();
            serviceDescriptors.AddGEItemAPI();
        }

        public void Configure()
        {

        }
    }
}