using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class Migration7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaSeriesEpisode_MediaSeriesSeasons_MediaSeriesSeasonId",
                table: "MediaSeriesEpisode");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaSeriesEpisode",
                table: "MediaSeriesEpisode");

            migrationBuilder.RenameTable(
                name: "MediaSeriesEpisode",
                newName: "MediaSeriesEpisodes");

            migrationBuilder.RenameIndex(
                name: "IX_MediaSeriesEpisode_MediaSeriesSeasonId",
                table: "MediaSeriesEpisodes",
                newName: "IX_MediaSeriesEpisodes_MediaSeriesSeasonId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSpecial",
                table: "MediaSeriesEpisodes",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaSeriesEpisodes",
                table: "MediaSeriesEpisodes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeriesEpisodes_Id",
                table: "MediaSeriesEpisodes",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaSeriesEpisodes_MediaSeriesSeasons_MediaSeriesSeasonId",
                table: "MediaSeriesEpisodes",
                column: "MediaSeriesSeasonId",
                principalTable: "MediaSeriesSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaSeriesEpisodes_MediaSeriesSeasons_MediaSeriesSeasonId",
                table: "MediaSeriesEpisodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaSeriesEpisodes",
                table: "MediaSeriesEpisodes");

            migrationBuilder.DropIndex(
                name: "IX_MediaSeriesEpisodes_Id",
                table: "MediaSeriesEpisodes");

            migrationBuilder.RenameTable(
                name: "MediaSeriesEpisodes",
                newName: "MediaSeriesEpisode");

            migrationBuilder.RenameIndex(
                name: "IX_MediaSeriesEpisodes_MediaSeriesSeasonId",
                table: "MediaSeriesEpisode",
                newName: "IX_MediaSeriesEpisode_MediaSeriesSeasonId");

            migrationBuilder.AlterColumn<bool>(
                name: "IsSpecial",
                table: "MediaSeriesEpisode",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaSeriesEpisode",
                table: "MediaSeriesEpisode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaSeriesEpisode_MediaSeriesSeasons_MediaSeriesSeasonId",
                table: "MediaSeriesEpisode",
                column: "MediaSeriesSeasonId",
                principalTable: "MediaSeriesSeasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
