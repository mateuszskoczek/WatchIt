﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WatchIt.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "accounts");

            migrationBuilder.EnsureSchema(
                name: "genders");

            migrationBuilder.EnsureSchema(
                name: "genres");

            migrationBuilder.EnsureSchema(
                name: "media");

            migrationBuilder.EnsureSchema(
                name: "people");

            migrationBuilder.EnsureSchema(
                name: "photos");

            migrationBuilder.EnsureSchema(
                name: "roles");

            migrationBuilder.CreateTable(
                name: "Genders",
                schema: "genders",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                schema: "genres",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Media",
                schema: "media",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    OriginalTitle = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Duration = table.Column<short>(type: "smallint", nullable: true),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    Budget = table.Column<decimal>(type: "money", nullable: true),
                    HasEnded = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleActorTypes",
                schema: "roles",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleActorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleCreatorTypes",
                schema: "roles",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleCreatorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    Password = table.Column<byte[]>(type: "bytea", maxLength: 1000, nullable: false),
                    LeftSalt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RightSalt = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsAdmin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    JoinDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ActiveDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    GenderId = table.Column<short>(type: "smallint", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Genders_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "genders",
                        principalTable: "Genders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "People",
                schema: "people",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DeathDate = table.Column<DateOnly>(type: "date", nullable: true),
                    GenderId = table.Column<short>(type: "smallint", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                    table.ForeignKey(
                        name: "FK_People_Genders_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "genders",
                        principalTable: "Genders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MediumGenres",
                schema: "media",
                columns: table => new
                {
                    MediumId = table.Column<long>(type: "bigint", nullable: false),
                    GenreId = table.Column<short>(type: "smallint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediumGenres", x => new { x.GenreId, x.MediumId });
                    table.ForeignKey(
                        name: "FK_MediumGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "genres",
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediumGenres_Media_MediumId",
                        column: x => x.MediumId,
                        principalSchema: "media",
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediumPictures",
                schema: "media",
                columns: table => new
                {
                    MediumId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: -1, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UploadDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediumPictures", x => x.MediumId);
                    table.ForeignKey(
                        name: "FK_MediumPictures_Media_MediumId",
                        column: x => x.MediumId,
                        principalSchema: "media",
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediumViewCounts",
                schema: "media",
                columns: table => new
                {
                    MediumId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "now()"),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediumViewCounts", x => new { x.MediumId, x.Date });
                    table.ForeignKey(
                        name: "FK_MediumViewCounts_Media_MediumId",
                        column: x => x.MediumId,
                        principalSchema: "media",
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                schema: "photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MediumId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: -1, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UploadDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Media_MediumId",
                        column: x => x.MediumId,
                        principalSchema: "media",
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountFollows",
                schema: "accounts",
                columns: table => new
                {
                    FollowerId = table.Column<long>(type: "bigint", nullable: false),
                    FollowedId = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFollows", x => new { x.FollowerId, x.FollowedId });
                    table.ForeignKey(
                        name: "FK_AccountFollows_Accounts_FollowedId",
                        column: x => x.FollowedId,
                        principalSchema: "accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountFollows_Accounts_FollowerId",
                        column: x => x.FollowerId,
                        principalSchema: "accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountProfilePictures",
                schema: "accounts",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: -1, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UploadDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountProfilePictures", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_AccountProfilePictures_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRefreshTokens",
                schema: "accounts",
                columns: table => new
                {
                    Token = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsExtendable = table.Column<bool>(type: "boolean", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_AccountRefreshTokens_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MediumRatings",
                schema: "media",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    MediumId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<byte>(type: "smallint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediumRatings", x => new { x.AccountId, x.MediumId });
                    table.ForeignKey(
                        name: "FK_MediumRatings_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediumRatings_Media_MediumId",
                        column: x => x.MediumId,
                        principalSchema: "media",
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonPictures",
                schema: "people",
                columns: table => new
                {
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: -1, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UploadDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPictures", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_PersonPictures_People_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "people",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonViewCounts",
                schema: "people",
                columns: table => new
                {
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "now()"),
                    ViewCount = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonViewCounts", x => new { x.PersonId, x.Date });
                    table.ForeignKey(
                        name: "FK_PersonViewCounts_People_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "people",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    MediumId = table.Column<long>(type: "bigint", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    ActorTypeId = table.Column<short>(type: "smallint", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatorTypeId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Media_MediumId",
                        column: x => x.MediumId,
                        principalSchema: "media",
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_People_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "people",
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_RoleActorTypes_ActorTypeId",
                        column: x => x.ActorTypeId,
                        principalSchema: "roles",
                        principalTable: "RoleActorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Roles_RoleCreatorTypes_CreatorTypeId",
                        column: x => x.CreatorTypeId,
                        principalSchema: "roles",
                        principalTable: "RoleCreatorTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoBackground",
                schema: "photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsUniversal = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    FirstGradientColor = table.Column<byte[]>(type: "bytea", nullable: false),
                    SecondGradientColor = table.Column<byte[]>(type: "bytea", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoBackground", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotoBackground_Photos_PhotoId",
                        column: x => x.PhotoId,
                        principalSchema: "photos",
                        principalTable: "Photos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleRatings",
                schema: "roles",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<byte>(type: "smallint", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRatings", x => new { x.AccountId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RoleRatings_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleRatings_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "roles",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountBackgroundPictures",
                schema: "accounts",
                columns: table => new
                {
                    AccountId = table.Column<long>(type: "bigint", nullable: false),
                    BackgroundId = table.Column<Guid>(type: "uuid", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountBackgroundPictures", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_AccountBackgroundPictures_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "accounts",
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountBackgroundPictures_PhotoBackground_BackgroundId",
                        column: x => x.BackgroundId,
                        principalSchema: "photos",
                        principalTable: "PhotoBackground",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountBackgroundPictures_AccountId",
                schema: "accounts",
                table: "AccountBackgroundPictures",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountBackgroundPictures_BackgroundId",
                schema: "accounts",
                table: "AccountBackgroundPictures",
                column: "BackgroundId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFollows_FollowedId",
                schema: "accounts",
                table: "AccountFollows",
                column: "FollowedId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountProfilePictures_AccountId",
                schema: "accounts",
                table: "AccountProfilePictures",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountRefreshTokens_AccountId",
                schema: "accounts",
                table: "AccountRefreshTokens",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRefreshTokens_Token",
                schema: "accounts",
                table: "AccountRefreshTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_GenderId",
                schema: "accounts",
                table: "Accounts",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Id",
                schema: "accounts",
                table: "Accounts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Id",
                schema: "genders",
                table: "Genders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genres_Id",
                schema: "genres",
                table: "Genres",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_Id",
                schema: "media",
                table: "Media",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediumGenres_MediumId",
                schema: "media",
                table: "MediumGenres",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_MediumPictures_MediumId",
                schema: "media",
                table: "MediumPictures",
                column: "MediumId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediumRatings_MediumId",
                schema: "media",
                table: "MediumRatings",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_People_GenderId",
                schema: "people",
                table: "People",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_People_Id",
                schema: "people",
                table: "People",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonPictures_PersonId",
                schema: "people",
                table: "PersonPictures",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBackground_Id",
                schema: "photos",
                table: "PhotoBackground",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhotoBackground_PhotoId",
                schema: "photos",
                table: "PhotoBackground",
                column: "PhotoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Id",
                schema: "photos",
                table: "Photos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_MediumId",
                schema: "photos",
                table: "Photos",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleActorTypes_Id",
                schema: "roles",
                table: "RoleActorTypes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleCreatorTypes_Id",
                schema: "roles",
                table: "RoleCreatorTypes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleRatings_RoleId",
                schema: "roles",
                table: "RoleRatings",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ActorTypeId",
                schema: "roles",
                table: "Roles",
                column: "ActorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatorTypeId",
                schema: "roles",
                table: "Roles",
                column: "CreatorTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Id",
                schema: "roles",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_MediumId",
                schema: "roles",
                table: "Roles",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_PersonId",
                schema: "roles",
                table: "Roles",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountBackgroundPictures",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "AccountFollows",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "AccountProfilePictures",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "AccountRefreshTokens",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "MediumGenres",
                schema: "media");

            migrationBuilder.DropTable(
                name: "MediumPictures",
                schema: "media");

            migrationBuilder.DropTable(
                name: "MediumRatings",
                schema: "media");

            migrationBuilder.DropTable(
                name: "MediumViewCounts",
                schema: "media");

            migrationBuilder.DropTable(
                name: "PersonPictures",
                schema: "people");

            migrationBuilder.DropTable(
                name: "PersonViewCounts",
                schema: "people");

            migrationBuilder.DropTable(
                name: "RoleRatings",
                schema: "roles");

            migrationBuilder.DropTable(
                name: "PhotoBackground",
                schema: "photos");

            migrationBuilder.DropTable(
                name: "Genres",
                schema: "genres");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "accounts");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "roles");

            migrationBuilder.DropTable(
                name: "Photos",
                schema: "photos");

            migrationBuilder.DropTable(
                name: "People",
                schema: "people");

            migrationBuilder.DropTable(
                name: "RoleActorTypes",
                schema: "roles");

            migrationBuilder.DropTable(
                name: "RoleCreatorTypes",
                schema: "roles");

            migrationBuilder.DropTable(
                name: "Media",
                schema: "media");

            migrationBuilder.DropTable(
                name: "Genders",
                schema: "genders");
        }
    }
}
