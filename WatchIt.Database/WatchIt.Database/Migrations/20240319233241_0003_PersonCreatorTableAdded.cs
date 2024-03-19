using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class _0003_PersonCreatorTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonCreatorRoleTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCreatorRoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonCreatorRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    MediaId = table.Column<long>(type: "bigint", nullable: false),
                    PersonCreatorRoleTypeId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonCreatorRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonCreatorRoles_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonCreatorRoles_PersonCreatorRoleTypes_PersonCreatorRole~",
                        column: x => x.PersonCreatorRoleTypeId,
                        principalTable: "PersonCreatorRoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonCreatorRoles_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PersonCreatorRoleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "Director" },
                    { (short)2, "Producer" },
                    { (short)3, "Screenwriter" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonCreatorRoles_Id",
                table: "PersonCreatorRoles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonCreatorRoles_MediaId",
                table: "PersonCreatorRoles",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCreatorRoles_PersonCreatorRoleTypeId",
                table: "PersonCreatorRoles",
                column: "PersonCreatorRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCreatorRoles_PersonId",
                table: "PersonCreatorRoles",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonCreatorRoleTypes_Id",
                table: "PersonCreatorRoleTypes",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonCreatorRoles");

            migrationBuilder.DropTable(
                name: "PersonCreatorRoleTypes");
        }
    }
}
