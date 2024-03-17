using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model;
using WatchIt.Database.Model.Account;
using WatchIt.Database.Model.Genre;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database
{
    public class DatabaseContext : DbContext
    {
        #region CONSTRUCTORS

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #endregion



        #region PROPERTIES

        // Account
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountProfilePicture> AccountProfilePictures { get; set; }

        // Genre
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<GenreMedia> GenresMedia { get; set; }

        // Media
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<MediaMovie> MediaMovies { get; set; }
        public virtual DbSet<MediaSeries> MediaSeries { get; set; }
        public virtual DbSet<MediaSeriesSeason> MediaSeriesSeasons { get; set; }
        public virtual DbSet<MediaSeriesEpisode> MediaSeriesEpisodes { get; set; }
        public virtual DbSet<MediaPosterImage> MediaPosterImages { get; set; }
        public virtual DbSet<MediaPhotoImage> MediaPhotoImages { get; set; }

        #endregion



        #region METHODS

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("name=Default");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            // Genre
            EntityBuilder.Build<Genre>(modelBuilder);
            EntityBuilder.Build<GenreMedia>(modelBuilder);
        }

        #endregion
    }
}
