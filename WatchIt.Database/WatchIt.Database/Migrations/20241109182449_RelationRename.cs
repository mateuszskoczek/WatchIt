using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class RelationRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountFollow_Accounts_AccountFollowedId",
                table: "AccountFollow");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountFollow_Accounts_AccountFollowerId",
                table: "AccountFollow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountFollow",
                table: "AccountFollow");

            migrationBuilder.RenameTable(
                name: "AccountFollow",
                newName: "AccountFollows");

            migrationBuilder.RenameIndex(
                name: "IX_AccountFollow_Id",
                table: "AccountFollows",
                newName: "IX_AccountFollows_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AccountFollow_AccountFollowerId",
                table: "AccountFollows",
                newName: "IX_AccountFollows_AccountFollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountFollow_AccountFollowedId",
                table: "AccountFollows",
                newName: "IX_AccountFollows_AccountFollowedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountFollows",
                table: "AccountFollows",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LeftSalt", "Password", "RightSalt" },
                values: new object[] { "QkJky0:m43g!mRL_-0S'", new byte[] { 104, 21, 33, 198, 192, 7, 229, 80, 195, 46, 190, 26, 125, 243, 113, 195, 194, 9, 145, 142, 56, 34, 125, 141, 133, 113, 14, 172, 29, 90, 194, 60, 98, 40, 121, 132, 218, 157, 80, 128, 70, 136, 201, 208, 36, 37, 124, 215, 144, 242, 212, 68, 209, 27, 248, 191, 212, 84, 250, 35, 230, 51, 218, 15 }, "~-jO$aMa{Q9lAW~>)Z+Z" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountFollows_Accounts_AccountFollowedId",
                table: "AccountFollows",
                column: "AccountFollowedId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountFollows_Accounts_AccountFollowerId",
                table: "AccountFollows",
                column: "AccountFollowerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountFollows_Accounts_AccountFollowedId",
                table: "AccountFollows");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountFollows_Accounts_AccountFollowerId",
                table: "AccountFollows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountFollows",
                table: "AccountFollows");

            migrationBuilder.RenameTable(
                name: "AccountFollows",
                newName: "AccountFollow");

            migrationBuilder.RenameIndex(
                name: "IX_AccountFollows_Id",
                table: "AccountFollow",
                newName: "IX_AccountFollow_Id");

            migrationBuilder.RenameIndex(
                name: "IX_AccountFollows_AccountFollowerId",
                table: "AccountFollow",
                newName: "IX_AccountFollow_AccountFollowerId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountFollows_AccountFollowedId",
                table: "AccountFollow",
                newName: "IX_AccountFollow_AccountFollowedId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountFollow",
                table: "AccountFollow",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LeftSalt", "Password", "RightSalt" },
                values: new object[] { "sfA:fW!3D=GbwXn]X+rm", new byte[] { 95, 134, 48, 126, 85, 131, 129, 152, 252, 161, 69, 133, 62, 112, 45, 111, 3, 163, 80, 99, 167, 66, 169, 121, 140, 69, 242, 14, 89, 126, 184, 43, 62, 87, 22, 1, 88, 246, 34, 181, 231, 110, 14, 54, 120, 114, 37, 67, 240, 82, 112, 125, 84, 155, 194, 90, 14, 189, 90, 68, 30, 146, 204, 105 }, "@$rr>fSvr5Ls|D+Wp;?D" });

            migrationBuilder.AddForeignKey(
                name: "FK_AccountFollow_Accounts_AccountFollowedId",
                table: "AccountFollow",
                column: "AccountFollowedId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountFollow_Accounts_AccountFollowerId",
                table: "AccountFollow",
                column: "AccountFollowerId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
