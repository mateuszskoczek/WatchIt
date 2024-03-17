using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMedia_Genres_GenreId",
                table: "GenreMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMedia_Media_MediaId",
                table: "GenreMedia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreMedia",
                table: "GenreMedia");

            migrationBuilder.RenameTable(
                name: "GenreMedia",
                newName: "GenresMedia");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMedia_MediaId",
                table: "GenresMedia",
                newName: "IX_GenresMedia_MediaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenresMedia",
                table: "GenresMedia",
                columns: new[] { "GenreId", "MediaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMedia_Genres_GenreId",
                table: "GenresMedia",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenresMedia_Media_MediaId",
                table: "GenresMedia",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenresMedia_Genres_GenreId",
                table: "GenresMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_GenresMedia_Media_MediaId",
                table: "GenresMedia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenresMedia",
                table: "GenresMedia");

            migrationBuilder.RenameTable(
                name: "GenresMedia",
                newName: "GenreMedia");

            migrationBuilder.RenameIndex(
                name: "IX_GenresMedia_MediaId",
                table: "GenreMedia",
                newName: "IX_GenreMedia_MediaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreMedia",
                table: "GenreMedia",
                columns: new[] { "GenreId", "MediaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMedia_Genres_GenreId",
                table: "GenreMedia",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMedia_Media_MediaId",
                table: "GenreMedia",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
