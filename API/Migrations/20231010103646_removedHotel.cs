using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class removedHotel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_Hotel_Hotelid",
                table: "Admin");

            migrationBuilder.DropForeignKey(
                name: "FK_Customer_Hotel_Hotelid",
                table: "Customer");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Hotel_Hotelid",
                table: "Room");

            migrationBuilder.DropTable(
                name: "Hotel");

            migrationBuilder.DropIndex(
                name: "IX_Room_Hotelid",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Customer_Hotelid",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Admin_Hotelid",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Hotelid",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "Hotelid",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Hotelid",
                table: "Admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Hotelid",
                table: "Room",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Hotelid",
                table: "Customer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Hotelid",
                table: "Admin",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hotel",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotel", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Room_Hotelid",
                table: "Room",
                column: "Hotelid");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Hotelid",
                table: "Customer",
                column: "Hotelid");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Hotelid",
                table: "Admin",
                column: "Hotelid");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_Hotel_Hotelid",
                table: "Admin",
                column: "Hotelid",
                principalTable: "Hotel",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_Hotel_Hotelid",
                table: "Customer",
                column: "Hotelid",
                principalTable: "Hotel",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Hotel_Hotelid",
                table: "Room",
                column: "Hotelid",
                principalTable: "Hotel",
                principalColumn: "id");
        }
    }
}
