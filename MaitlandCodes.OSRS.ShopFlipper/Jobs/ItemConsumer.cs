using Hangfire;
using MAD.Extensions.EFCore;
using MaitlandCodes.OSRS.GEItemAPI;
using MaitlandCodes.OSRS.GEItemAPI.Models;
using MaitlandCodes.OSRS.ShopFlipper.Database;
using MIFCore.Hangfire;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaitlandCodes.OSRS.ShopFlipper.Jobs
{
    public class ItemConsumer
    {
        private readonly IGEItemAPIClient apiClient;
        private readonly ShopFlipperDbContext dbContext;
        private readonly IBackgroundJobClient backgroundJobClient;

        public ItemConsumer(IGEItemAPIClient apiClient, ShopFlipperDbContext dbContext, IBackgroundJobClient backgroundJobClient)
        {
            this.apiClient = apiClient;
            this.dbContext = dbContext;
            this.backgroundJobClient = backgroundJobClient;
        }

        [DisableConcurrentExecution(600)]
        public async Task ConsumeItems(ItemCategory itemCategory, string alpha, int page = 1)
        {
            var itemsResult = await apiClient.GetItems(itemCategory, alpha, page);

            if (itemsResult.Total == 0
                || itemsResult.Items.Any() == false)
            {
                return;
            }

            foreach (var itm in itemsResult.Items)
            {
                this.dbContext.Upsert(itm, e =>
                {
                    if (e is Item i)
                    {
                        this.dbContext.Entry(i).Property("Category").CurrentValue = itemCategory;
                    }
                });
            }

            await this.dbContext.SaveChangesAsync();

            this.backgroundJobClient.Enqueue<ItemConsumer>(y => y.ConsumeItems(itemCategory, alpha, page + 1));
        }
    }
}
