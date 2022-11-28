using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Catering.Data.Migrations
{
    public partial class editedFoodItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "FoodItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "FoodItems",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 1,
                column: "Category",
                value: "Side");

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 2,
                column: "Category",
                value: "Entree");
        }
    }
}
