using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Catering.Data.Migrations
{
    public partial class initMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "INTEGER", maxLength: 3, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Category = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UnitPrice = table.Column<int>(type: "INTEGER", nullable: false),
                    IsVegan = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.FoodItemId);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", maxLength: 3, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MenuName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "Foodbookings",
                columns: table => new
                {
                    FoodBookingId = table.Column<int>(type: "INTEGER", maxLength: 3, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClientReferenceId = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "INTEGER", nullable: false),
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foodbookings", x => x.FoodBookingId);
                    table.ForeignKey(
                        name: "FK_Foodbookings_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuFoodItems",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFoodItems", x => new { x.FoodItemId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_MenuFoodItems_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuFoodItems_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "FoodItemId", "Category", "DateCreated", "Description", "IsVegan", "Title", "UnitPrice" },
                values: new object[] { 1, "Side", new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "lovely chips", true, "chips", 250 });

            migrationBuilder.InsertData(
                table: "FoodItems",
                columns: new[] { "FoodItemId", "Category", "DateCreated", "Description", "IsVegan", "Title", "UnitPrice" },
                values: new object[] { 2, "Entree", new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "just a sosig", false, "sosig", 300 });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "MenuId", "DateCreated", "MenuName" },
                values: new object[] { 1, new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Breakfast Menu" });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "MenuId", "DateCreated", "MenuName" },
                values: new object[] { 2, new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brunch Menu" });

            migrationBuilder.InsertData(
                table: "MenuFoodItems",
                columns: new[] { "FoodItemId", "MenuId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "MenuFoodItems",
                columns: new[] { "FoodItemId", "MenuId" },
                values: new object[] { 2, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Foodbookings_MenuId",
                table: "Foodbookings",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoodItems_MenuId",
                table: "MenuFoodItems",
                column: "MenuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foodbookings");

            migrationBuilder.DropTable(
                name: "MenuFoodItems");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "Menu");
        }
    }
}
