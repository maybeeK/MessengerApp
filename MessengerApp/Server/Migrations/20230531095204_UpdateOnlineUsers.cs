using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessengerApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnlineUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OnlineUserConnection",
                table: "OnlineUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnlineUserConnection",
                table: "OnlineUsers");
        }
    }
}
