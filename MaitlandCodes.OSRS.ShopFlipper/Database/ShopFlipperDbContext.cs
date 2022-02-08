using Microsoft.EntityFrameworkCore;
using MaitlandCodes.OSRS.GEItemAPI.Models;
using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MaitlandCodes.OSRS.WikiClient.Models;

namespace MaitlandCodes.OSRS.ShopFlipper.Database
{
    public class ShopFlipperDbContext : DbContext
    {
        public ShopFlipperDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CatalogueResult>(cfg =>
            {
                cfg.Property<ItemCategory>("Category")
                    .IsRequired()
                    .HasConversion(new EnumToStringConverter<ItemCategory>());

                cfg.HasKey("Category");

                cfg.Property(y => y.Types).HasConversion(
                    y => y.ToJsonString(null),
                    y => JsonArray.Create(JsonDocument.Parse(y, default).RootElement, null));

                cfg.OwnsMany(y => y.Alpha, cfg =>
                {
                    cfg.WithOwner().HasForeignKey("Category");
                    cfg.HasKey("Category", "Letter");
                });
            });

            modelBuilder.Entity<Item>(cfg =>
            {
                cfg.Property<ItemCategory>("Category")
                    .IsRequired()
                    .HasConversion(new EnumToStringConverter<ItemCategory>());

                cfg.HasKey(y => y.Id);
                cfg.Property(y => y.Id).ValueGeneratedNever();

                cfg.OwnsOne(y => y.Current, cfg =>
                {
                    cfg.Property(y => y.Price).HasConversion(
                        y => y.ToString(),
                        y => JsonSerializer.SerializeToElement(y, null as JsonSerializerOptions));
                });

                cfg.OwnsOne(y => y.Today, cfg =>
                {
                    cfg.Property(y => y.Price).HasConversion(
                        y => y.ToString(),
                        y => JsonSerializer.SerializeToElement(y, null as JsonSerializerOptions));
                });
            });

            modelBuilder.Entity<StoreLocation>(cfg =>
            {
                cfg.OwnsOne(y => y.Location, cfg =>
                {
                    cfg.Ignore(y => y.Title);
                    cfg.Property(y => y.Uri).HasColumnName("LocationUri");
                });

                cfg.OwnsOne(y => y.Seller, cfg =>
                {
                    cfg.Ignore(y => y.Title);
                    cfg.Property(y => y.Uri).HasColumnName("SellerUri");
                });

                cfg.Property<string>("LocationName").IsRequired();
                cfg.Property<string>("SellerName").IsRequired();
                cfg.Property<long>("ItemId").IsRequired();

                cfg.HasKey("ItemId", "LocationName", "SellerName");
            });
        }
    }
}
