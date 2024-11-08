using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class GenreDescriptionRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Genres");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LeftSalt", "Password", "RightSalt" },
                values: new object[] { "HIZYSU/94oe$[jAy\\{7V", new byte[] { 118, 180, 133, 0, 25, 6, 65, 230, 20, 221, 180, 8, 111, 189, 191, 158, 98, 160, 80, 196, 146, 99, 90, 55, 196, 219, 245, 244, 167, 89, 123, 74, 37, 92, 234, 81, 74, 199, 149, 128, 7, 213, 202, 191, 162, 62, 19, 144, 206, 83, 80, 237, 37, 179, 12, 215, 61, 179, 94, 189, 30, 98, 100, 164 }, "#(^8YBkY;<X=LKE_7$2p" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Genres",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "LeftSalt", "Password", "RightSalt" },
                values: new object[] { "YuJiv1\"R'*0odl8${\\|S", new byte[] { 215, 154, 186, 191, 12, 223, 76, 105, 137, 67, 41, 138, 26, 3, 38, 36, 0, 71, 40, 84, 153, 152, 105, 239, 55, 60, 164, 15, 99, 175, 133, 175, 227, 245, 102, 9, 171, 119, 16, 234, 97, 179, 70, 29, 120, 112, 241, 91, 209, 91, 228, 164, 52, 244, 36, 207, 147, 60, 124, 66, 77, 252, 129, 151 }, "oT2N=y7^5,2o'+N>d}~!" });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: (short)1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: (short)2,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: (short)3,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: (short)4,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: (short)5,
                column: "Description",
                value: null);
        }
    }
}
