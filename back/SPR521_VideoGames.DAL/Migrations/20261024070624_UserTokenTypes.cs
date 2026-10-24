using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SPR521_VideoGames.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UserTokenTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewEmail",
                table: "UserTokens",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "UserTokens",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewEmail",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "UserTokens");
        }
    }
}
