﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WatchIt.Database;

#nullable disable

namespace WatchIt.Database.Migrations
{
    [DbContext(typeof(DatabaseContextNew))]
    [Migration("20241111010548_MediumTables")]
    partial class MediumTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTimeOffset>("ActiveDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("character varying(320)");

                    b.Property<short?>("GenderId")
                        .HasColumnType("smallint");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTimeOffset>("JoinDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("LeftSalt")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("bytea");

                    b.Property<string>("RightSalt")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Accounts", "accounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountFollow", b =>
                {
                    b.Property<long>("FollowerId")
                        .HasColumnType("bigint");

                    b.Property<long>("FollowedId")
                        .HasColumnType("bigint");

                    b.HasKey("FollowerId", "FollowedId");

                    b.HasIndex("FollowedId");

                    b.ToTable("AccountFollows", "accounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountProfilePicture", b =>
                {
                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasMaxLength(-1)
                        .HasColumnType("bytea");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("UploadDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("AccountProfilePictures", "accounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountRefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsExtendable")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("AccountRefreshTokens", "accounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Genders.Gender", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Genders", "genders");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Genres.Genre", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Genres", "genres");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.Medium", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<long?>("Duration")
                        .HasColumnType("bigint");

                    b.Property<string>("OriginalTitle")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateTimeOffset?>("ReleaseDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Media", "media");

                    b.HasDiscriminator<byte>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumGenre", b =>
                {
                    b.Property<short>("GenreId")
                        .HasColumnType("smallint");

                    b.Property<long>("MediumId")
                        .HasColumnType("bigint");

                    b.HasKey("GenreId", "MediumId");

                    b.HasIndex("MediumId");

                    b.ToTable("MediumGenres", "media");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumMovie", b =>
                {
                    b.HasBaseType("WatchIt.Database.Model.Media.Medium");

                    b.Property<decimal?>("Budget")
                        .HasColumnType("money");

                    b.HasDiscriminator().HasValue((byte)0);
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumSeries", b =>
                {
                    b.HasBaseType("WatchIt.Database.Model.Media.Medium");

                    b.Property<bool>("HasEnded")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.HasDiscriminator().HasValue((byte)1);
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.Account", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Genders.Gender", "Gender")
                        .WithMany("Accounts")
                        .HasForeignKey("GenderId");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountFollow", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Accounts.Account", "Followed")
                        .WithMany("FollowersRelationshipObjects")
                        .HasForeignKey("FollowedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchIt.Database.Model.Accounts.Account", "Follower")
                        .WithMany("FollowsRelationshipObjects")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Followed");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountProfilePicture", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Accounts.Account", "Account")
                        .WithOne("ProfilePicture")
                        .HasForeignKey("WatchIt.Database.Model.Accounts.AccountProfilePicture", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountRefreshToken", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Accounts.Account", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumGenre", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Genres.Genre", "Genre")
                        .WithMany("MediaRelationObjects")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchIt.Database.Model.Media.Medium", "Medium")
                        .WithMany("GenresRelationshipObjects")
                        .HasForeignKey("MediumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Medium");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.Account", b =>
                {
                    b.Navigation("FollowersRelationshipObjects");

                    b.Navigation("FollowsRelationshipObjects");

                    b.Navigation("ProfilePicture");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Genders.Gender", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Genres.Genre", b =>
                {
                    b.Navigation("MediaRelationObjects");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.Medium", b =>
                {
                    b.Navigation("GenresRelationshipObjects");
                });
#pragma warning restore 612, 618
        }
    }
}
