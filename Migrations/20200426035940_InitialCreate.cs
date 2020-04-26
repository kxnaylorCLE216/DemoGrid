using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExampleGrid.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerTB",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Phoneno = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTB", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Country = table.Column<string>(nullable: true),
                    Product = table.Column<string>(nullable: true),
                    DiscountBand = table.Column<string>(nullable: true),
                    UnitsSold = table.Column<int>(nullable: false),
                    ManufacturingPrice = table.Column<decimal>(nullable: false),
                    SalePrice = table.Column<decimal>(nullable: false),
                    GrossSales = table.Column<decimal>(nullable: false),
                    Discounts = table.Column<decimal>(nullable: false),
                    Sales = table.Column<decimal>(nullable: false),
                    COGS = table.Column<decimal>(nullable: false),
                    Profit = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EnteredBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerTB");

            migrationBuilder.DropTable(
                name: "Samples");
        }
    }
}
