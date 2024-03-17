using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class Migration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaSeriesSeason_MediaSeries_MediaSeriesId",
                table: "MediaSeriesSeason");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaSeriesSeason",
                table: "MediaSeriesSeason");

            migrationBuilder.RenameTable(
                name: "MediaSeriesSeason",
                newName: "MediaSeriesSeasons");

            migrationBuilder.RenameIndex(
                name: "IX_MediaSeriesSeason_MediaSeriesId",
                table: "MediaSeriesSeasons",
                newName: "IX_MediaSeriesSeasons_MediaSeriesId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSpecial",
                table: "MediaSeriesSeasons",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaSeriesSeasons",
                table: "MediaSeriesSeasons",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeriesSeasons_Id",
                table: "MediaSeriesSeasons",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaSeriesSeasons_MediaSeries_MediaSeriesId",
                table: "MediaSeriesSeasons",
                column: "MediaSeriesId",
                principalTable: "MediaSeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaSeriesSeasons_MediaSeries_MediaSeriesId",
                table: "MediaSeriesSeasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaSeriesSeasons",
                table: "MediaSeriesSeasons");

            migrationBuilder.DropIndex(
                name: "IX_MediaSeriesSeasons_Id",
                table: "MediaSeriesSeasons");

            migrationBuilder.RenameTable(
                name: "MediaSeriesSeasons",
                newName: "MediaSeriesSeason");

            migrationBuilder.RenameIndex(
                name: "IX_MediaSeriesSeasons_MediaSeriesId",
                table: "MediaSeriesSeason",
                newName: "IX_MediaSeriesSeason_MediaSeriesId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSpecial",
                table: "MediaSeriesSeason",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaSeriesSeason",
                table: "MediaSeriesSeason",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaSeriesSeason_MediaSeries_MediaSeriesId",
                table: "MediaSeriesSeason",
                column: "MediaSeriesId",
                principalTable: "MediaSeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
