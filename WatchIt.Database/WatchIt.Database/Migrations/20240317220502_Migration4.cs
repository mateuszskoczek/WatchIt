using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaSeries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HasEnded = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaSeries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaSeriesSeason",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaSeriesId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsSpecial = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaSeriesSeason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaSeriesSeason_MediaSeries_MediaSeriesId",
                        column: x => x.MediaSeriesId,
                        principalTable: "MediaSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeries_Id",
                table: "MediaSeries",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeriesSeason_MediaSeriesId",
                table: "MediaSeriesSeason",
                column: "MediaSeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_MediaSeries_Id",
                table: "Media",
                column: "Id",
                principalTable: "MediaSeries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_MediaSeries_Id",
                table: "Media");

            migrationBuilder.DropTable(
                name: "MediaSeriesSeason");

            migrationBuilder.DropTable(
                name: "MediaSeries");
        }
    }
}
