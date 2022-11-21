using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    public partial class AddedSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "StaffId", "Adress", "ContactEmail", "ContactNumber", "Forename", "StartDate", "Surname", "payRollNumber" },
                values: new object[] { 1, "75 high street", null, "07532759859", "thomas", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wintersgill", "223" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "StaffId",
                keyValue: 1);
        }
    }
}
