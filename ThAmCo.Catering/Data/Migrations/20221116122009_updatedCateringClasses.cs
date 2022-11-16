using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Catering.Data.Migrations
{
    public partial class updatedCateringClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Menu",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FoodItems",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "FoodItems",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "FoodItems",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsVegan",
                table: "FoodItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FoodItems",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 1,
                columns: new[] { "Category", "DateCreated", "Description", "IsVegan", "Title", "UnitPrice" },
                values: new object[] { "Side", new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "lovely chips", true, "chips", 250 });

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 2,
                columns: new[] { "Category", "DateCreated", "Description", "Title", "UnitPrice" },
                values: new object[] { "Entree", new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "just a sosig", "sosig", 300 });

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "MenuId",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Menu",
                keyColumn: "MenuId",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2015, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Menu");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "IsVegan",
                table: "FoodItems");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "FoodItems");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "FoodItems",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 1,
                columns: new[] { "Description", "UnitPrice" },
                values: new object[] { "chips", 8 });

            migrationBuilder.UpdateData(
                table: "FoodItems",
                keyColumn: "FoodItemId",
                keyValue: 2,
                columns: new[] { "Description", "UnitPrice" },
                values: new object[] { "sosig", 6 });
        }
    }
}
