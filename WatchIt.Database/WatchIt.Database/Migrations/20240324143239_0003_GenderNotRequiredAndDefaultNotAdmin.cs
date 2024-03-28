using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class _0003_GenderNotRequiredAndDefaultNotAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Genders_GenderId",
                table: "Accounts");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<short>(
                name: "GenderId",
                table: "Accounts",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Genders_GenderId",
                table: "Accounts",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Genders_GenderId",
                table: "Accounts");

            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Accounts",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<short>(
                name: "GenderId",
                table: "Accounts",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Genders_GenderId",
                table: "Accounts",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
