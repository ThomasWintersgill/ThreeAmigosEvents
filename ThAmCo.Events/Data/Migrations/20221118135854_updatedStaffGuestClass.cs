using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    public partial class updatedStaffGuestClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "ForeName",
                table: "Guests",
                newName: "Forename");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Staff",
                type: "TEXT",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Forename",
                table: "Staff",
                type: "TEXT",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adress",
                table: "Staff",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Staff",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Staff",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Staff",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "payRollNumber",
                table: "Staff",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Guests",
                type: "TEXT",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Forename",
                table: "Guests",
                type: "TEXT",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactAdress",
                table: "Guests",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Guests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactNumber",
                table: "Guests",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adress",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "payRollNumber",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "ContactAdress",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "Guests");

            migrationBuilder.RenameColumn(
                name: "Forename",
                table: "Guests",
                newName: "ForeName");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Staff",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Forename",
                table: "Staff",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Guests",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "ForeName",
                table: "Guests",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 25);

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestId", "ForeName", "Surname" },
                values: new object[] { 1, "Thomas", "Wintersgill" });
        }
    }
}
