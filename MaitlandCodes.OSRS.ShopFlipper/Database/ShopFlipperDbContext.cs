using Microsoft.EntityFrameworkCore;
using MaitlandCodes.OSRS.GEItemAPI.Models;
using System.Text.Json.Nodes;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MaitlandCodes.OSRS.ShopFlipper.Database
{
    internal class ShopFlipperDbContext : DbContext
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
                        y => JsonDocument.Parse(y, default).RootElement);
                });

                cfg.OwnsOne(y => y.Today, cfg =>
                {
                    cfg.Property(y => y.Price).HasConversion(
                        y => y.ToString(),
                        y => JsonDocument.Parse(y, default).RootElement);
                });
            });
        }
    }
}
