﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WatchIt.Database;

#nullable disable

namespace WatchIt.Database.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

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

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Accounts", "accounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountBackgroundPicture", b =>
                {
                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("BackgroundId")
                        .HasColumnType("uuid");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("BackgroundId");

                    b.ToTable("AccountBackgroundPictures", "accounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountFollow", b =>
                {
                    b.Property<long>("FollowerId")
                        .HasColumnType("bigint");

                    b.Property<long>("FollowedId")
                        .HasColumnType("bigint");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

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

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("AccountProfilePictures", "accounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountRefreshToken", b =>
                {
                    b.Property<Guid>("Token")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("ExpirationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsExtendable")
                        .HasColumnType("boolean");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Token");

                    b.HasIndex("AccountId");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.ToTable("AccountRefreshTokens", "accounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Genders.Gender", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<short>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

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

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<short>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

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

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<short?>("Duration")
                        .HasColumnType("smallint");

                    b.Property<string>("OriginalTitle")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<DateOnly?>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

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

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("GenreId", "MediumId");

                    b.HasIndex("MediumId");

                    b.ToTable("MediumGenres", "media");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumPicture", b =>
                {
                    b.Property<long>("MediumId")
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

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("MediumId");

                    b.HasIndex("MediumId")
                        .IsUnique();

                    b.ToTable("MediumPictures", "media");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumRating", b =>
                {
                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("MediumId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<byte>("Rating")
                        .HasColumnType("smallint");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("AccountId", "MediumId");

                    b.HasIndex("MediumId");

                    b.ToTable("MediumRatings", "media");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumViewCount", b =>
                {
                    b.Property<long>("MediumId")
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("now()");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<long>("ViewCount")
                        .HasColumnType("bigint");

                    b.HasKey("MediumId", "Date");

                    b.ToTable("MediumViewCounts", "media");
                });

            modelBuilder.Entity("WatchIt.Database.Model.People.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<long>("Id"));

                    b.Property<DateOnly?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DeathDate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("FullName")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<short?>("GenderId")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("People", "people");
                });

            modelBuilder.Entity("WatchIt.Database.Model.People.PersonPicture", b =>
                {
                    b.Property<long>("PersonId")
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

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("PersonId");

                    b.HasIndex("PersonId")
                        .IsUnique();

                    b.ToTable("PersonPictures", "people");
                });

            modelBuilder.Entity("WatchIt.Database.Model.People.PersonViewCount", b =>
                {
                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<DateOnly>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("now()");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.Property<long>("ViewCount")
                        .HasColumnType("bigint");

                    b.HasKey("PersonId", "Date");

                    b.ToTable("PersonViewCounts", "people");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Photos.Photo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasMaxLength(-1)
                        .HasColumnType("bytea");

                    b.Property<long>("MediumId")
                        .HasColumnType("bigint");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTimeOffset>("UploadDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("MediumId");

                    b.ToTable("Photos", "photos");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Photos.PhotoBackground", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<byte[]>("FirstGradientColor")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<bool>("IsUniversal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<Guid>("PhotoId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("SecondGradientColor")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("PhotoId")
                        .IsUnique();

                    b.ToTable("PhotoBackground", "photos");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("MediumId")
                        .HasColumnType("bigint");

                    b.Property<long>("PersonId")
                        .HasColumnType("bigint");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.HasIndex("MediumId");

                    b.HasIndex("PersonId");

                    b.ToTable("Roles", "roles");

                    b.HasDiscriminator<byte>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleActorType", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<short>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("RoleActorTypes", "roles");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleCreatorType", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<short>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("RoleCreatorTypes", "roles");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleRating", b =>
                {
                    b.Property<long>("AccountId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<byte>("Rating")
                        .HasColumnType("smallint");

                    b.Property<uint>("Version")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("AccountId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleRatings", "roles");
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

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleActor", b =>
                {
                    b.HasBaseType("WatchIt.Database.Model.Roles.Role");

                    b.Property<short>("ActorTypeId")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasIndex("ActorTypeId");

                    b.HasDiscriminator().HasValue((byte)0);
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleCreator", b =>
                {
                    b.HasBaseType("WatchIt.Database.Model.Roles.Role");

                    b.Property<short>("CreatorTypeId")
                        .HasColumnType("smallint");

                    b.HasIndex("CreatorTypeId");

                    b.HasDiscriminator().HasValue((byte)1);
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.Account", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Genders.Gender", "Gender")
                        .WithMany("Accounts")
                        .HasForeignKey("GenderId");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.AccountBackgroundPicture", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Accounts.Account", "Account")
                        .WithOne("BackgroundPicture")
                        .HasForeignKey("WatchIt.Database.Model.Accounts.AccountBackgroundPicture", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchIt.Database.Model.Photos.PhotoBackground", "Background")
                        .WithMany("BackgroundUsages")
                        .HasForeignKey("BackgroundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Background");
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

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumPicture", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Media.Medium", "Medium")
                        .WithOne("Picture")
                        .HasForeignKey("WatchIt.Database.Model.Media.MediumPicture", "MediumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medium");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumRating", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Accounts.Account", "Account")
                        .WithMany("MediaRatings")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchIt.Database.Model.Media.Medium", "Medium")
                        .WithMany("Ratings")
                        .HasForeignKey("MediumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Medium");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.MediumViewCount", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Media.Medium", "Medium")
                        .WithMany("ViewCounts")
                        .HasForeignKey("MediumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medium");
                });

            modelBuilder.Entity("WatchIt.Database.Model.People.Person", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Genders.Gender", "Gender")
                        .WithMany("People")
                        .HasForeignKey("GenderId");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("WatchIt.Database.Model.People.PersonPicture", b =>
                {
                    b.HasOne("WatchIt.Database.Model.People.Person", "Person")
                        .WithOne("Picture")
                        .HasForeignKey("WatchIt.Database.Model.People.PersonPicture", "PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("WatchIt.Database.Model.People.PersonViewCount", b =>
                {
                    b.HasOne("WatchIt.Database.Model.People.Person", "Person")
                        .WithMany("ViewCounts")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Photos.Photo", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Media.Medium", "Medium")
                        .WithMany("Photos")
                        .HasForeignKey("MediumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medium");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Photos.PhotoBackground", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Photos.Photo", "Photo")
                        .WithOne("Background")
                        .HasForeignKey("WatchIt.Database.Model.Photos.PhotoBackground", "PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.Role", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Media.Medium", "Medium")
                        .WithMany("Roles")
                        .HasForeignKey("MediumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchIt.Database.Model.People.Person", "Person")
                        .WithMany("Roles")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medium");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleRating", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Accounts.Account", "Account")
                        .WithMany("RolesRatings")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WatchIt.Database.Model.Roles.Role", "Role")
                        .WithMany("Ratings")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleActor", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Roles.RoleActorType", "ActorType")
                        .WithMany("Roles")
                        .HasForeignKey("ActorTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActorType");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleCreator", b =>
                {
                    b.HasOne("WatchIt.Database.Model.Roles.RoleCreatorType", "CreatorType")
                        .WithMany("Roles")
                        .HasForeignKey("CreatorTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorType");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Accounts.Account", b =>
                {
                    b.Navigation("BackgroundPicture");

                    b.Navigation("FollowersRelationshipObjects");

                    b.Navigation("FollowsRelationshipObjects");

                    b.Navigation("MediaRatings");

                    b.Navigation("ProfilePicture");

                    b.Navigation("RefreshTokens");

                    b.Navigation("RolesRatings");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Genders.Gender", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("People");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Genres.Genre", b =>
                {
                    b.Navigation("MediaRelationObjects");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Media.Medium", b =>
                {
                    b.Navigation("GenresRelationshipObjects");

                    b.Navigation("Photos");

                    b.Navigation("Picture");

                    b.Navigation("Ratings");

                    b.Navigation("Roles");

                    b.Navigation("ViewCounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.People.Person", b =>
                {
                    b.Navigation("Picture");

                    b.Navigation("Roles");

                    b.Navigation("ViewCounts");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Photos.Photo", b =>
                {
                    b.Navigation("Background");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Photos.PhotoBackground", b =>
                {
                    b.Navigation("BackgroundUsages");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.Role", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleActorType", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("WatchIt.Database.Model.Roles.RoleCreatorType", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
