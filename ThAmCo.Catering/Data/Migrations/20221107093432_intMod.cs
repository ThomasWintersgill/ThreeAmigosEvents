using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Catering.Data.Migrations
{
    public partial class intMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UnitPrice",
                table: "FoodItems",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 1,
                column: "UnitPrice",
                value: 8);

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 2,
                column: "UnitPrice",
                value: 6);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "UnitPrice",
                table: "FoodItems",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 1,
                column: "UnitPrice",
                value: 8.0);

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 2,
                column: "UnitPrice",
                value: 6.0);
        }
    }
}
