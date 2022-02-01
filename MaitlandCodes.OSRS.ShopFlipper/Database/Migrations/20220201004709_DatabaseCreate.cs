using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaitlandCodes.OSRS.ShopFlipper.Database.Migrations
{
    public partial class DatabaseCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogueResult",
                columns: table => new
                {
                    Category = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Types = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogueResult", x => x.Category);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconLarge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Current_Trend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Current_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Today_Trend = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Today_Price = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Members = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alpha",
                columns: table => new
                {
                    Letter = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Items = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alpha", x => new { x.Category, x.Letter });
                    table.ForeignKey(
                        name: "FK_Alpha_CatalogueResult_Category",
                        column: x => x.Category,
                        principalTable: "CatalogueResult",
                        principalColumn: "Category",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alpha");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "CatalogueResult");
        }
    }
}
