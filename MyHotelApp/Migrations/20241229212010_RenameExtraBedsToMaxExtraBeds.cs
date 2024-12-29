using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyHotelApp.Migrations
{
    /// <inheritdoc />
    public partial class RenameExtraBedsToMaxExtraBeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                           name: "ExtraBeds",       
                           table: "DoubleRooms",    
                           newName: "MaxExtraBeds"  
                       );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaxExtraBeds",     
                table: "DoubleRooms",    
                newName: "ExtraBeds"     
            );
        }
    }
}
