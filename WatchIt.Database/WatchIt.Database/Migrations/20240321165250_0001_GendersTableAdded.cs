using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class _0001_GendersTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Gender_GenderId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Gender_GenderId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gender",
                table: "Gender");

            migrationBuilder.RenameTable(
                name: "Gender",
                newName: "Genders");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genders",
                table: "Genders",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "Male" },
                    { (short)2, "Female" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Id",
                table: "Genders",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Genders_GenderId",
                table: "Accounts",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Genders_GenderId",
                table: "Persons",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Genders_GenderId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Genders_GenderId",
                table: "Persons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genders",
                table: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Genders_Id",
                table: "Genders");

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "Genders",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.RenameTable(
                name: "Genders",
                newName: "Gender");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gender",
                table: "Gender",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Gender_GenderId",
                table: "Accounts",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Gender_GenderId",
                table: "Persons",
                column: "GenderId",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
