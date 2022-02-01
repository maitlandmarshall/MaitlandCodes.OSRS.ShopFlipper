using Hangfire;
using MAD.Extensions.EFCore;
using MaitlandCodes.OSRS.GEItemAPI;
using MaitlandCodes.OSRS.GEItemAPI.Models;
using MaitlandCodes.OSRS.ShopFlipper.Database;
using MIFCore.Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaitlandCodes.OSRS.ShopFlipper.Jobs
{
    public class CatalogueConsumer
    {
        private readonly IGEItemAPIClient apiClient;
        private readonly ShopFlipperDbContext dbContext;
        private readonly IBackgroundJobClient backgroundJobClient;

        public CatalogueConsumer(IGEItemAPIClient apiClient, ShopFlipperDbContext dbContext, IBackgroundJobClient backgroundJobClient)
        {
            this.apiClient = apiClient;
            this.dbContext = dbContext;
            this.backgroundJobClient = backgroundJobClient;
        }

        public Task EnqueueCategoriesToConsume()
        {
            var itemCategories = Enum.GetValues<ItemCategory>();

            foreach (var i in itemCategories)
            {
                this.backgroundJobClient.Enqueue<CatalogueConsumer>(y => y.ConsumeCatalogue(i));
            }

            return Task.CompletedTask;
        }

        [DisableIdenticalQueuedItems]
        public async Task ConsumeCatalogue(ItemCategory itemCategory)
        {
            var categories = await apiClient.GetCatalogues(itemCategory);
            dbContext.Entry(categories).Property("Category").CurrentValue = itemCategory;

            this.dbContext.Upsert(categories);
            await this.dbContext.SaveChangesAsync();

            foreach (var a in categories.Alpha)
            {
                if (a.Items == 0)
                    continue;

                this.backgroundJobClient.Enqueue<ItemConsumer>(y => y.ConsumeItems(itemCategory, a.Letter, 1));
            }
        }
    }
}
