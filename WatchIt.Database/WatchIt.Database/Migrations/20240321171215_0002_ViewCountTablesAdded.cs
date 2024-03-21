using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class _0002_ViewCountTablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ViewCountsMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "now()"),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewCountsMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewCountsMedia_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViewCountsPerson",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "now()"),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewCountsPerson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ViewCountsPerson_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ViewCountsMedia_Id",
                table: "ViewCountsMedia",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ViewCountsMedia_MediaId",
                table: "ViewCountsMedia",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_ViewCountsPerson_Id",
                table: "ViewCountsPerson",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ViewCountsPerson_PersonId",
                table: "ViewCountsPerson",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ViewCountsMedia");

            migrationBuilder.DropTable(
                name: "ViewCountsPerson");
        }
    }
}
