using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaitlandCodes.OSRS.ShopFlipper.Database.Migrations
{
    public partial class Add_StoreLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreLocation",
                columns: table => new
                {
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    LocationName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SellerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Item = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    RestockTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellPrice = table.Column<int>(type: "int", nullable: false),
                    BuyPrice = table.Column<int>(type: "int", nullable: false),
                    IsMembers = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreLocation", x => new { x.ItemId, x.LocationName, x.SellerName });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreLocation");
        }
    }
}
