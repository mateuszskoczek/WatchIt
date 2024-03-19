using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class _0000_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountProfilePictures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: -1, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProfilePictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaMovies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Budget = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaMovies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaPosterImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: -1, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPosterImages", x => x.Id);
                });

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
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    OriginalTitle = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Length = table.Column<TimeSpan>(type: "interval", nullable: true),
                    MediaPosterImageId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_MediaMovies_Id",
                        column: x => x.Id,
                        principalTable: "MediaMovies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Media_MediaPosterImages_MediaPosterImageId",
                        column: x => x.MediaPosterImageId,
                        principalTable: "MediaPosterImages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Media_MediaSeries_Id",
                        column: x => x.Id,
                        principalTable: "MediaSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaSeriesSeasons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaSeriesId = table.Column<long>(type: "bigint", nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaSeriesSeasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaSeriesSeasons_MediaSeries_MediaSeriesId",
                        column: x => x.MediaSeriesId,
                        principalTable: "MediaSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaGenres",
                columns: table => new
                {
                    MediaId = table.Column<long>(type: "bigint", nullable: false),
                    GenreId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaGenres", x => new { x.GenreId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_MediaGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaGenres_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaPhotoImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: -1, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    IsMediaBackground = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsUniversalBackground = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaPhotoImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaPhotoImages_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaProductionCountrys",
                columns: table => new
                {
                    MediaId = table.Column<long>(type: "bigint", nullable: false),
                    CountryId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaProductionCountrys", x => new { x.CountryId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_MediaProductionCountrys_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaProductionCountrys_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediaSeriesEpisodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaSeriesSeasonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsSpecial = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaSeriesEpisodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaSeriesEpisodes_MediaSeriesSeasons_MediaSeriesSeasonId",
                        column: x => x.MediaSeriesSeasonId,
                        principalTable: "MediaSeriesSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    ProfilePictureId = table.Column<Guid>(type: "uuid", nullable: true),
                    BackgroundPictureId = table.Column<Guid>(type: "uuid", nullable: true),
                    Password = table.Column<byte[]>(type: "bytea", maxLength: 1000, nullable: false),
                    LeftSalt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RightSalt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    LastActive = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountProfilePictures_ProfilePictureId",
                        column: x => x.ProfilePictureId,
                        principalTable: "AccountProfilePictures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_MediaPhotoImages_BackgroundPictureId",
                        column: x => x.BackgroundPictureId,
                        principalTable: "MediaPhotoImages",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "Afghanistan" },
                    { (short)2, "Albania" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { (short)1, null, "Comedy" },
                    { (short)2, null, "Thriller" },
                    { (short)3, null, "Horror" },
                    { (short)4, null, "Action" },
                    { (short)5, null, "Drama" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountProfilePictures_Id",
                table: "AccountProfilePictures",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BackgroundPictureId",
                table: "Accounts",
                column: "BackgroundPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id",
                table: "Accounts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ProfilePictureId",
                table: "Accounts",
                column: "ProfilePictureId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Id",
                table: "Countries",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Id",
                table: "Genres",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_Id",
                table: "Media",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_MediaPosterImageId",
                table: "Media",
                column: "MediaPosterImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaGenres_MediaId",
                table: "MediaGenres",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaMovies_Id",
                table: "MediaMovies",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaPhotoImages_Id",
                table: "MediaPhotoImages",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaPhotoImages_MediaId",
                table: "MediaPhotoImages",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaPosterImages_Id",
                table: "MediaPosterImages",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaProductionCountrys_MediaId",
                table: "MediaProductionCountrys",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeries_Id",
                table: "MediaSeries",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeriesEpisodes_Id",
                table: "MediaSeriesEpisodes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeriesEpisodes_MediaSeriesSeasonId",
                table: "MediaSeriesEpisodes",
                column: "MediaSeriesSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeriesSeasons_Id",
                table: "MediaSeriesSeasons",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaSeriesSeasons_MediaSeriesId",
                table: "MediaSeriesSeasons",
                column: "MediaSeriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "MediaGenres");

            migrationBuilder.DropTable(
                name: "MediaProductionCountrys");

            migrationBuilder.DropTable(
                name: "MediaSeriesEpisodes");

            migrationBuilder.DropTable(
                name: "AccountProfilePictures");

            migrationBuilder.DropTable(
                name: "MediaPhotoImages");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "MediaSeriesSeasons");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "MediaMovies");

            migrationBuilder.DropTable(
                name: "MediaPosterImages");

            migrationBuilder.DropTable(
                name: "MediaSeries");
        }
    }
}
