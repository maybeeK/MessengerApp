using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessengerApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddOnlineUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnlineUsers",
                columns: table => new
                {
                    OnlineUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineUsers", x => x.OnlineUserId);
                    table.ForeignKey(
                        name: "FK_OnlineUsers_AspNetUsers_OnlineUserId",
                        column: x => x.OnlineUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlineUsers");
        }
    }
}
