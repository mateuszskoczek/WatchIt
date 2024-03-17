using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class Migration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSpecial",
                table: "MediaSeriesSeasons");

            migrationBuilder.CreateTable(
                name: "MediaSeriesEpisode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaSeriesSeasonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsSpecial = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaSeriesEpisode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaSeriesEpisode_MediaSeriesSeasons_MediaSeriesSeasonId",
                        column: x => x.MediaSeriesSeasonId,
                        principalTable: "MediaSeriesSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeriesEpisode_MediaSeriesSeasonId",
                table: "MediaSeriesEpisode",
                column: "MediaSeriesSeasonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaSeriesEpisode");

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecial",
                table: "MediaSeriesSeasons",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
