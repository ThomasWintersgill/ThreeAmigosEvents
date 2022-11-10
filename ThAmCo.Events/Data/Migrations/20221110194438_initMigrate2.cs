using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    public partial class initMigrate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FoodBookingId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FoodItem",
                columns: table => new
                {
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    UnitPrice = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItem", x => x.FoodItemId);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", maxLength: 3, nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MenuName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "FoodBooking",
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
                    table.PrimaryKey("PK_FoodBooking", x => x.FoodBookingId);
                    table.ForeignKey(
                        name: "FK_FoodBooking_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuFoodItem",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "INTEGER", nullable: false),
                    FoodItemId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuFoodItem", x => new { x.FoodItemId, x.MenuId });
                    table.ForeignKey(
                        name: "FK_MenuFoodItem_FoodItem_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItem",
                        principalColumn: "FoodItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuFoodItem_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "MenuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_FoodBookingId",
                table: "Events",
                column: "FoodBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodBooking_MenuId",
                table: "FoodBooking",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuFoodItem_MenuId",
                table: "MenuFoodItem",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_FoodBooking_FoodBookingId",
                table: "Events",
                column: "FoodBookingId",
                principalTable: "FoodBooking",
                principalColumn: "FoodBookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_FoodBooking_FoodBookingId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "FoodBooking");

            migrationBuilder.DropTable(
                name: "MenuFoodItem");

            migrationBuilder.DropTable(
                name: "FoodItem");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropIndex(
                name: "IX_Events_FoodBookingId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FoodBookingId",
                table: "Events");
        }
    }
}
