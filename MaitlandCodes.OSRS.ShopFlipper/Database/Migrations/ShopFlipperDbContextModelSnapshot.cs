﻿// <auto-generated />
using MaitlandCodes.OSRS.ShopFlipper.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MaitlandCodes.OSRS.ShopFlipper.Database.Migrations
{
    [DbContext(typeof(ShopFlipperDbContext))]
    partial class ShopFlipperDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MaitlandCodes.OSRS.GEItemAPI.Models.CatalogueResult", b =>
                {
                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Types")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Category");

                    b.ToTable("CatalogueResult");
                });

            modelBuilder.Entity("MaitlandCodes.OSRS.GEItemAPI.Models.Item", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconLarge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Members")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeIcon")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("MaitlandCodes.OSRS.WikiClient.Models.StoreLocation", b =>
                {
                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<string>("LocationName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SellerName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BuyPrice")
                        .HasColumnType("int");

                    b.Property<bool>("IsMembers")
                        .HasColumnType("bit");

                    b.Property<string>("Item")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RestockTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SellPrice")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "LocationName", "SellerName");

                    b.ToTable("StoreLocation");
                });

            modelBuilder.Entity("MaitlandCodes.OSRS.GEItemAPI.Models.CatalogueResult", b =>
                {
                    b.OwnsMany("MaitlandCodes.OSRS.GEItemAPI.Models.Alpha", "Alpha", b1 =>
                        {
                            b1.Property<string>("Category")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Letter")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<long>("Items")
                                .HasColumnType("bigint");

                            b1.HasKey("Category", "Letter");

                            b1.ToTable("Alpha");

                            b1.WithOwner()
                                .HasForeignKey("Category");
                        });

                    b.Navigation("Alpha");
                });

            modelBuilder.Entity("MaitlandCodes.OSRS.GEItemAPI.Models.Item", b =>
                {
                    b.OwnsOne("MaitlandCodes.OSRS.GEItemAPI.Models.PriceTrend", "Current", b1 =>
                        {
                            b1.Property<long>("ItemId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Price")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Trend")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ItemId");

                            b1.ToTable("Item");

                            b1.WithOwner()
                                .HasForeignKey("ItemId");
                        });

                    b.OwnsOne("MaitlandCodes.OSRS.GEItemAPI.Models.PriceTrend", "Today", b1 =>
                        {
                            b1.Property<long>("ItemId")
                                .HasColumnType("bigint");

                            b1.Property<string>("Price")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Trend")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ItemId");

                            b1.ToTable("Item");

                            b1.WithOwner()
                                .HasForeignKey("ItemId");
                        });

                    b.Navigation("Current");

                    b.Navigation("Today");
                });

            modelBuilder.Entity("MaitlandCodes.OSRS.WikiClient.Models.StoreLocation", b =>
                {
                    b.OwnsOne("MaitlandCodes.OSRS.WikiClient.Models.UriWithTitle", "Location", b1 =>
                        {
                            b1.Property<long>("StoreLocationItemId")
                                .HasColumnType("bigint");

                            b1.Property<string>("StoreLocationLocationName")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("StoreLocationSellerName")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Uri")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("LocationUri");

                            b1.HasKey("StoreLocationItemId", "StoreLocationLocationName", "StoreLocationSellerName");

                            b1.ToTable("StoreLocation");

                            b1.WithOwner()
                                .HasForeignKey("StoreLocationItemId", "StoreLocationLocationName", "StoreLocationSellerName");
                        });

                    b.OwnsOne("MaitlandCodes.OSRS.WikiClient.Models.UriWithTitle", "Seller", b1 =>
                        {
                            b1.Property<long>("StoreLocationItemId")
                                .HasColumnType("bigint");

                            b1.Property<string>("StoreLocationLocationName")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("StoreLocationSellerName")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Uri")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("SellerUri");

                            b1.HasKey("StoreLocationItemId", "StoreLocationLocationName", "StoreLocationSellerName");

                            b1.ToTable("StoreLocation");

                            b1.WithOwner()
                                .HasForeignKey("StoreLocationItemId", "StoreLocationLocationName", "StoreLocationSellerName");
                        });

                    b.Navigation("Location");

                    b.Navigation("Seller");
                });
#pragma warning restore 612, 618
        }
    }
}
