using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThAmCo.Events.Data.Migrations
{
    public partial class changedStaffModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StaffName",
                table: "Staff",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "Forename",
                table: "Staff",
                type: "TEXT",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Guests",
                columns: new[] { "GuestId", "ForeName", "Surname" },
                values: new object[] { 1, "Thomas", "Wintersgill" });

            migrationBuilder.CreateIndex(
                name: "IX_FoodBooking_ClientReferenceId",
                table: "FoodBooking",
                column: "ClientReferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodBooking_Events_ClientReferenceId",
                table: "FoodBooking",
                column: "ClientReferenceId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodBooking_Events_ClientReferenceId",
                table: "FoodBooking");

            migrationBuilder.DropIndex(
                name: "IX_FoodBooking_ClientReferenceId",
                table: "FoodBooking");

            migrationBuilder.DeleteData(
                table: "Guests",
                keyColumn: "GuestId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Forename",
                table: "Staff");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Staff",
                newName: "StaffName");
        }
    }
}
