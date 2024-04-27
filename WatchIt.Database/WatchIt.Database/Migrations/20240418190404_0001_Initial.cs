using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class _0001_Initial : Migration
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
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsHistorical = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
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
                name: "PersonPhotoImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: -1, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhotoImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    OriginalTitle = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Length = table.Column<short>(type: "smallint", nullable: true),
                    MediaPosterImageId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Media_MediaPosterImages_MediaPosterImageId",
                        column: x => x.MediaPosterImageId,
                        principalTable: "MediaPosterImages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DeathDate = table.Column<DateOnly>(type: "date", nullable: true),
                    GenderId = table.Column<short>(type: "smallint", nullable: true),
                    PersonPhotoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Persons_PersonPhotoImages_PersonPhotoId",
                        column: x => x.PersonPhotoId,
                        principalTable: "PersonPhotoImages",
                        principalColumn: "Id");
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
                name: "MediaMovies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Budget = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaMovies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaMovies_Media_Id",
                        column: x => x.Id,
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
                name: "MediaSeries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    HasEnded = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MediaSeries_Media_Id",
                        column: x => x.Id,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    GenderId = table.Column<short>(type: "smallint", nullable: true),
                    ProfilePictureId = table.Column<Guid>(type: "uuid", nullable: true),
                    BackgroundPictureId = table.Column<Guid>(type: "uuid", nullable: true),
                    Password = table.Column<byte[]>(type: "bytea", maxLength: 1000, nullable: false),
                    LeftSalt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RightSalt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
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
                        name: "FK_Accounts_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accounts_MediaPhotoImages_BackgroundPictureId",
                        column: x => x.BackgroundPictureId,
                        principalTable: "MediaPhotoImages",
                        principalColumn: "Id");
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
                name: "AccountRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsExtendable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountRefreshTokens_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingsMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaId = table.Column<long>(type: "bigint", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingsMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingsMedia_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingsMedia_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingsPersonActorRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonActorRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingsPersonActorRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingsPersonActorRole_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingsPersonActorRole_PersonActorRoles_PersonActorRoleId",
                        column: x => x.PersonActorRoleId,
                        principalTable: "PersonActorRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingsPersonCreatorRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonCreatorRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingsPersonCreatorRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingsPersonCreatorRole_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingsPersonCreatorRole_PersonCreatorRoles_PersonCreatorRo~",
                        column: x => x.PersonCreatorRoleId,
                        principalTable: "PersonCreatorRoles",
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
                name: "RatingsMediaSeriesSeason",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaSeriesSeasonId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingsMediaSeriesSeason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingsMediaSeriesSeason_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingsMediaSeriesSeason_MediaSeriesSeasons_MediaSeriesSeas~",
                        column: x => x.MediaSeriesSeasonId,
                        principalTable: "MediaSeriesSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingsMediaSeriesEpisode",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaSeriesEpisodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingsMediaSeriesEpisode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingsMediaSeriesEpisode_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingsMediaSeriesEpisode_MediaSeriesEpisodes_MediaSeriesEp~",
                        column: x => x.MediaSeriesEpisodeId,
                        principalTable: "MediaSeriesEpisodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "BackgroundPictureId", "Description", "Email", "GenderId", "IsAdmin", "LeftSalt", "Password", "ProfilePictureId", "RightSalt", "Username" },
                values: new object[] { 1L, null, null, "root@watch.it", null, true, "qE]Q^g%tU\"6Uu^GfE:V:", new byte[] { 165, 250, 135, 31, 187, 161, 15, 246, 18, 232, 64, 25, 37, 173, 91, 111, 140, 177, 183, 84, 254, 177, 15, 235, 119, 219, 29, 169, 32, 108, 187, 121, 204, 51, 213, 28, 141, 89, 91, 226, 0, 23, 7, 91, 139, 230, 151, 104, 62, 91, 59, 6, 207, 26, 200, 141, 104, 5, 151, 201, 243, 163, 28, 248 }, null, "T7j)~.#%~ZtOFUZFK,K+", "root" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "Afghanistan" },
                    { (short)2, "Albania" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "Male" },
                    { (short)2, "Female" }
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

            migrationBuilder.InsertData(
                table: "PersonActorRoleTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { (short)1, "Actor" },
                    { (short)2, "Supporting actor" },
                    { (short)3, "Voice actor" }
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
                name: "IX_AccountProfilePictures_Id",
                table: "AccountProfilePictures",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRefreshTokens_AccountId",
                table: "AccountRefreshTokens",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRefreshTokens_Id",
                table: "AccountRefreshTokens",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BackgroundPictureId",
                table: "Accounts",
                column: "BackgroundPictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_GenderId",
                table: "Accounts",
                column: "GenderId");

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
                name: "IX_Genders_Id",
                table: "Genders",
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

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhotoImages_Id",
                table: "PersonPhotoImages",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_GenderId",
                table: "Persons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Id",
                table: "Persons",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PersonPhotoId",
                table: "Persons",
                column: "PersonPhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatingsMedia_AccountId",
                table: "RatingsMedia",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsMedia_Id",
                table: "RatingsMedia",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatingsMedia_MediaId",
                table: "RatingsMedia",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsMediaSeriesEpisode_AccountId",
                table: "RatingsMediaSeriesEpisode",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsMediaSeriesEpisode_Id",
                table: "RatingsMediaSeriesEpisode",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatingsMediaSeriesEpisode_MediaSeriesEpisodeId",
                table: "RatingsMediaSeriesEpisode",
                column: "MediaSeriesEpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsMediaSeriesSeason_AccountId",
                table: "RatingsMediaSeriesSeason",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsMediaSeriesSeason_Id",
                table: "RatingsMediaSeriesSeason",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatingsMediaSeriesSeason_MediaSeriesSeasonId",
                table: "RatingsMediaSeriesSeason",
                column: "MediaSeriesSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsPersonActorRole_AccountId",
                table: "RatingsPersonActorRole",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsPersonActorRole_Id",
                table: "RatingsPersonActorRole",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatingsPersonActorRole_PersonActorRoleId",
                table: "RatingsPersonActorRole",
                column: "PersonActorRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsPersonCreatorRole_AccountId",
                table: "RatingsPersonCreatorRole",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingsPersonCreatorRole_Id",
                table: "RatingsPersonCreatorRole",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RatingsPersonCreatorRole_PersonCreatorRoleId",
                table: "RatingsPersonCreatorRole",
                column: "PersonCreatorRoleId");

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
                name: "AccountRefreshTokens");

            migrationBuilder.DropTable(
                name: "MediaGenres");

            migrationBuilder.DropTable(
                name: "MediaMovies");

            migrationBuilder.DropTable(
                name: "MediaProductionCountrys");

            migrationBuilder.DropTable(
                name: "RatingsMedia");

            migrationBuilder.DropTable(
                name: "RatingsMediaSeriesEpisode");

            migrationBuilder.DropTable(
                name: "RatingsMediaSeriesSeason");

            migrationBuilder.DropTable(
                name: "RatingsPersonActorRole");

            migrationBuilder.DropTable(
                name: "RatingsPersonCreatorRole");

            migrationBuilder.DropTable(
                name: "ViewCountsMedia");

            migrationBuilder.DropTable(
                name: "ViewCountsPerson");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "MediaSeriesEpisodes");

            migrationBuilder.DropTable(
                name: "PersonActorRoles");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "PersonCreatorRoles");

            migrationBuilder.DropTable(
                name: "MediaSeriesSeasons");

            migrationBuilder.DropTable(
                name: "PersonActorRoleTypes");

            migrationBuilder.DropTable(
                name: "AccountProfilePictures");

            migrationBuilder.DropTable(
                name: "MediaPhotoImages");

            migrationBuilder.DropTable(
                name: "PersonCreatorRoleTypes");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "MediaSeries");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "PersonPhotoImages");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "MediaPosterImages");
        }
    }
}
