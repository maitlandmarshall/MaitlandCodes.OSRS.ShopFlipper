using Hangfire;
using MAD.Extensions.EFCore;
using MaitlandCodes.OSRS.GEItemAPI.Models;
using MaitlandCodes.OSRS.ShopFlipper.Database;
using MaitlandCodes.OSRS.WikiClient;
using MaitlandCodes.OSRS.WikiClient.Models;
using MIFCore.Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaitlandCodes.OSRS.ShopFlipper.Jobs
{
    internal class StoreLocationConsumer
    {
        private readonly OSRSStoreLocationClient wikiClient;
        private readonly ShopFlipperDbContext dbContext;

        public StoreLocationConsumer(OSRSStoreLocationClient wikiClient, ShopFlipperDbContext dbContext)
        {
            this.wikiClient = wikiClient;
            this.dbContext = dbContext;
        }

        [DisableConcurrentExecution(600)]
        [DisableIdenticalQueuedItems]
        public async Task ConsumeShopLocation(long itemId)
        {
            var item = await this.dbContext.FindAsync<Item>(itemId);

            if (item is null)
                return;

            var storeLocations = await this.wikiClient.GetStoreLocations(item.Name);

            if (storeLocations.Any() == false)
                return;

            foreach (var sl in storeLocations)
            {
                this.dbContext.Upsert(sl, entity =>
                {
                    if (entity is StoreLocation sl)
                    {
                        var entry = this.dbContext.Entry(sl);

                        entry.Property("ItemId").CurrentValue = item.Id;
                        entry.Property("LocationName").CurrentValue = sl.Location.Title;
                        entry.Property("SellerName").CurrentValue = sl.Seller.Title;
                    }
                });
            }

            await this.dbContext.SaveChangesAsync();
        }
    }
}
