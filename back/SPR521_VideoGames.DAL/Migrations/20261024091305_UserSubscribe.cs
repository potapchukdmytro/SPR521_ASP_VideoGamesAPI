using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPR521_VideoGames.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserSubscribe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribe",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscribe",
                table: "Users");
        }
    }
}
