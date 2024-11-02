using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class RatingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "RatingsPersonCreatorRole",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "RatingsPersonActorRole",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "RatingsMediaSeriesSeason",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "RatingsMediaSeriesEpisode",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "RatingsMedia",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now()");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "PersonActorRoles",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "RatingsPersonCreatorRole");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RatingsPersonActorRole");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RatingsMediaSeriesSeason");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RatingsMediaSeriesEpisode");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "RatingsMedia");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "PersonActorRoles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
