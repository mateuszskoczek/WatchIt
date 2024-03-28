using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class _0006_AccountRefreshTokenChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lifetime",
                table: "AccountRefreshTokens",
                newName: "ExpirationDate");

            migrationBuilder.AddColumn<bool>(
                name: "IsExtendable",
                table: "AccountRefreshTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExtendable",
                table: "AccountRefreshTokens");

            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "AccountRefreshTokens",
                newName: "Lifetime");
        }
    }
}
