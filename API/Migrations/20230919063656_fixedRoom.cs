using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class fixedRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Booking_bookingid",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_bookingid",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "bookingid",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "vacancy",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "Roomid",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Roomid",
                table: "Booking",
                column: "Roomid");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Room_Roomid",
                table: "Booking",
                column: "Roomid",
                principalTable: "Room",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Room_Roomid",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_Roomid",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Roomid",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "bookingid",
                table: "Room",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "vacancy",
                table: "Room",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Room_bookingid",
                table: "Room",
                column: "bookingid");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Booking_bookingid",
                table: "Room",
                column: "bookingid",
                principalTable: "Booking",
                principalColumn: "id");
        }
    }
}
