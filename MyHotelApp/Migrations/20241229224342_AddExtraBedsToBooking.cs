using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHotelApp.Migrations
{
    /// <inheritdoc />
    public partial class AddExtraBedsToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExtraBeds",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraBeds",
                table: "Bookings");
        }
    }
}
