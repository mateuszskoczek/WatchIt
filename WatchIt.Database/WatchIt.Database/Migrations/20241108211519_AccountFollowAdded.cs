using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class AccountFollowAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountFollow",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountFollowerId = table.Column<long>(type: "bigint", nullable: false),
                    AccountFollowedId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFollow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountFollow_Accounts_AccountFollowedId",
                        column: x => x.AccountFollowedId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountFollow_Accounts_AccountFollowerId",
                        column: x => x.AccountFollowerId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LeftSalt", "Password", "RightSalt" },
                values: new object[] { "sfA:fW!3D=GbwXn]X+rm", new byte[] { 95, 134, 48, 126, 85, 131, 129, 152, 252, 161, 69, 133, 62, 112, 45, 111, 3, 163, 80, 99, 167, 66, 169, 121, 140, 69, 242, 14, 89, 126, 184, 43, 62, 87, 22, 1, 88, 246, 34, 181, 231, 110, 14, 54, 120, 114, 37, 67, 240, 82, 112, 125, 84, 155, 194, 90, 14, 189, 90, 68, 30, 146, 204, 105 }, "@$rr>fSvr5Ls|D+Wp;?D" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountFollow_AccountFollowedId",
                table: "AccountFollow",
                column: "AccountFollowedId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFollow_AccountFollowerId",
                table: "AccountFollow",
                column: "AccountFollowerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFollow_Id",
                table: "AccountFollow",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountFollow");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LeftSalt", "Password", "RightSalt" },
                values: new object[] { "HIZYSU/94oe$[jAy\\{7V", new byte[] { 118, 180, 133, 0, 25, 6, 65, 230, 20, 221, 180, 8, 111, 189, 191, 158, 98, 160, 80, 196, 146, 99, 90, 55, 196, 219, 245, 244, 167, 89, 123, 74, 37, 92, 234, 81, 74, 199, 149, 128, 7, 213, 202, 191, 162, 62, 19, 144, 206, 83, 80, 237, 37, 179, 12, 215, 61, 179, 94, 189, 30, 98, 100, 164 }, "#(^8YBkY;<X=LKE_7$2p" });
        }
    }
}
