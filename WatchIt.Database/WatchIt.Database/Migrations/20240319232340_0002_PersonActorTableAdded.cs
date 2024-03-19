using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class _0002_PersonActorTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonActorRoleTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonActorRoleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonActorRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    MediaId = table.Column<long>(type: "bigint", nullable: false),
                    PersonActorRoleTypeId = table.Column<short>(type: "smallint", nullable: false),
                    RoleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonActorRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonActorRoles_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonActorRoles_PersonActorRoleTypes_PersonActorRoleTypeId",
                        column: x => x.PersonActorRoleTypeId,
                        principalTable: "PersonActorRoleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonActorRoles_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PersonActorRoleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "Actor" },
                    { (short)2, "Supporting actor" },
                    { (short)3, "Voice actor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonActorRoles_Id",
                table: "PersonActorRoles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonActorRoles_MediaId",
                table: "PersonActorRoles",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonActorRoles_PersonActorRoleTypeId",
                table: "PersonActorRoles",
                column: "PersonActorRoleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonActorRoles_PersonId",
                table: "PersonActorRoles",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonActorRoleTypes_Id",
                table: "PersonActorRoleTypes",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonActorRoles");

            migrationBuilder.DropTable(
                name: "PersonActorRoleTypes");
        }
    }
}
