using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPhotoImages_MediaPhotoImageBackgrounds_Id",
                table: "MediaPhotoImages");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaPhotoImages_MediaPhotoImageBackgrounds_MediaPhotoImage~",
                table: "MediaPhotoImages");

            migrationBuilder.DropIndex(
                name: "IX_MediaPhotoImages_MediaPhotoImageBackgroundId",
                table: "MediaPhotoImages");

            migrationBuilder.DropColumn(
                name: "MediaPhotoImageBackgroundId",
                table: "MediaPhotoImages");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LeftSalt", "Password", "RightSalt" },
                values: new object[] { "@(0PF{b6Ot?HO*:yF5`L", new byte[] { 254, 122, 19, 59, 187, 100, 174, 87, 55, 108, 14, 10, 123, 186, 129, 243, 145, 136, 152, 220, 72, 170, 196, 93, 54, 88, 192, 115, 128, 76, 133, 9, 181, 99, 181, 8, 102, 123, 197, 251, 85, 167, 146, 28, 116, 249, 118, 87, 146, 8, 194, 238, 127, 19, 33, 28, 14, 222, 218, 170, 74, 40, 223, 232 }, "=pt,3T0#CfC1[}Zfp{/u" });

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPhotoImageBackgrounds_MediaPhotoImages_Id",
                table: "MediaPhotoImageBackgrounds",
                column: "Id",
                principalTable: "MediaPhotoImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaPhotoImageBackgrounds_MediaPhotoImages_Id",
                table: "MediaPhotoImageBackgrounds");

            migrationBuilder.AddColumn<Guid>(
                name: "MediaPhotoImageBackgroundId",
                table: "MediaPhotoImages",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LeftSalt", "Password", "RightSalt" },
                values: new object[] { "Y&%]J>6Nc3&5~UUXnNxq", new byte[] { 68, 170, 8, 113, 134, 47, 98, 43, 96, 183, 126, 130, 204, 45, 4, 113, 81, 200, 244, 26, 54, 88, 161, 246, 84, 93, 159, 219, 12, 143, 128, 160, 198, 194, 47, 133, 216, 242, 158, 184, 43, 38, 134, 132, 175, 179, 42, 40, 0, 143, 111, 252, 156, 215, 17, 185, 12, 109, 119, 214, 211, 167, 32, 121 }, "MV1jo~o3Oa^;NWb\\Q)t_" });

            migrationBuilder.CreateIndex(
                name: "IX_MediaPhotoImages_MediaPhotoImageBackgroundId",
                table: "MediaPhotoImages",
                column: "MediaPhotoImageBackgroundId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPhotoImages_MediaPhotoImageBackgrounds_Id",
                table: "MediaPhotoImages",
                column: "Id",
                principalTable: "MediaPhotoImageBackgrounds",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaPhotoImages_MediaPhotoImageBackgrounds_MediaPhotoImage~",
                table: "MediaPhotoImages",
                column: "MediaPhotoImageBackgroundId",
                principalTable: "MediaPhotoImageBackgrounds",
                principalColumn: "Id");
        }
    }
}
