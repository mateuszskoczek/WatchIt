﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model;
using WatchIt.Database.Model.Account;
using WatchIt.Database.Model.Common;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Person;
using WatchIt.Database.Model.Rating;
using WatchIt.Database.Model.ViewCount;

namespace WatchIt.Database
{
    public class DatabaseContext : DbContext
    {
        #region CONSTRUCTORS

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #endregion



        #region PROPERTIES

        // Common
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }

        // Account
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountProfilePicture> AccountProfilePictures { get; set; }

        // Media
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<MediaMovie> MediaMovies { get; set; }
        public virtual DbSet<MediaSeries> MediaSeries { get; set; }
        public virtual DbSet<MediaSeriesSeason> MediaSeriesSeasons { get; set; }
        public virtual DbSet<MediaSeriesEpisode> MediaSeriesEpisodes { get; set; }
        public virtual DbSet<MediaPosterImage> MediaPosterImages { get; set; }
        public virtual DbSet<MediaPhotoImage> MediaPhotoImages { get; set; }
        public virtual DbSet<MediaGenre> MediaGenres { get; set; }
        public virtual DbSet<MediaProductionCountry> MediaProductionCountrys { get; set; }

        // Person
        public virtual DbSet<Person> Persons { get; set; }
        public virtual DbSet<PersonPhotoImage> PersonPhotoImages { get; set; }
        public virtual DbSet<PersonActorRole> PersonActorRoles { get; set; }
        public virtual DbSet<PersonActorRoleType> PersonActorRoleTypes { get; set; }
        public virtual DbSet<PersonCreatorRole> PersonCreatorRoles { get; set; }
        public virtual DbSet<PersonCreatorRoleType> PersonCreatorRoleTypes { get; set; }

        // Rating
        public virtual DbSet<RatingMedia> RatingsMedia { get; set; }
        public virtual DbSet<RatingPersonActorRole> RatingsPersonActorRole { get; set; }
        public virtual DbSet<RatingPersonActorRole> RatingsPersonCreatorRole { get; set; }
        public virtual DbSet<RatingMediaSeriesSeason> RatingsMediaSeriesSeason { get; set; }
        public virtual DbSet<RatingMediaSeriesEpisode> RatingsMediaSeriesEpisode { get; set; }

        // ViewCount
        public virtual DbSet<ViewCountPerson> ViewCountsPerson { get; set; }
        public virtual DbSet<ViewCountMedia> ViewCountsMedia { get; set; }


        #endregion



        #region METHODS

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("name=Default");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Common
            EntityBuilder.Build<Country>(modelBuilder);
            EntityBuilder.Build<Genre>(modelBuilder);
            EntityBuilder.Build<Gender>(modelBuilder);

            // Account
            EntityBuilder.Build<Account>(modelBuilder);
            EntityBuilder.Build<AccountProfilePicture>(modelBuilder);

            // Media
            EntityBuilder.Build<Media>(modelBuilder);
            EntityBuilder.Build<MediaMovie>(modelBuilder);
            EntityBuilder.Build<MediaSeries>(modelBuilder);
            EntityBuilder.Build<MediaSeriesSeason>(modelBuilder);
            EntityBuilder.Build<MediaSeriesEpisode>(modelBuilder);
            EntityBuilder.Build<MediaPosterImage>(modelBuilder);
            EntityBuilder.Build<MediaPhotoImage>(modelBuilder);
            EntityBuilder.Build<MediaGenre>(modelBuilder);
            EntityBuilder.Build<MediaProductionCountry>(modelBuilder);

            // Person
            EntityBuilder.Build<Person>(modelBuilder);
            EntityBuilder.Build<PersonPhotoImage>(modelBuilder);
            EntityBuilder.Build<PersonActorRole>(modelBuilder);
            EntityBuilder.Build<PersonActorRoleType>(modelBuilder);
            EntityBuilder.Build<PersonCreatorRole>(modelBuilder);
            EntityBuilder.Build<PersonCreatorRoleType>(modelBuilder);

            // Rating
            EntityBuilder.Build<RatingMedia>(modelBuilder);
            EntityBuilder.Build<RatingPersonActorRole>(modelBuilder);
            EntityBuilder.Build<RatingPersonCreatorRole>(modelBuilder);
            EntityBuilder.Build<RatingMediaSeriesSeason>(modelBuilder);
            EntityBuilder.Build<RatingMediaSeriesEpisode>(modelBuilder);

            // ViewCounts
            EntityBuilder.Build<ViewCountMedia>(modelBuilder);
            EntityBuilder.Build<ViewCountPerson>(modelBuilder);
        }

        #endregion
    }
}
