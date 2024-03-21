using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model.Common;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Account
{
    public class Account : IEntity<Account>
    {
        #region PROPERTIES

        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public short GenderId { get; set; }
        public Guid? ProfilePictureId { get; set; }
        public Guid? BackgroundPictureId { get; set; }
        public byte[] Password { get; set; }
        public string LeftSalt { get; set; }
        public string RightSalt { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTime CreationDate { get; set; }
        public DateTime LastActive { get; set; }

        #endregion



        #region NAVIGATION

        public Gender Gender { get; set; }
        public AccountProfilePicture? ProfilePicture { get; set; }
        public MediaPhotoImage? BackgroundPicture { get; set; }

        public IEnumerable<RatingMedia> RatingMedia { get; set; }
        public IEnumerable<RatingPersonActorRole> RatingPersonActorRole { get; set; }
        public IEnumerable<RatingPersonCreatorRole> RatingPersonCreatorRole { get; set; }
        public IEnumerable<RatingMediaSeriesSeason> RatingMediaSeriesSeason { get; set; }
        public IEnumerable<RatingMediaSeriesEpisode> RatingMediaSeriesEpisode { get; set; }

        #endregion



        #region METHODS

        static void IEntity<Account>.Build(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Username)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Email)
                   .HasMaxLength(320)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(1000);

            builder.HasOne(x => x.Gender)
                   .WithMany()
                   .HasForeignKey(x => x.GenderId)
                   .IsRequired();
            builder.Property(x => x.GenderId)
                   .IsRequired();

            builder.HasOne(x => x.ProfilePicture)
                   .WithOne(x => x.Account)
                   .HasForeignKey<Account>(e => e.ProfilePictureId);
            builder.Property(x => x.ProfilePictureId);

            builder.HasOne(x => x.BackgroundPicture)
                   .WithMany()
                   .HasForeignKey(x => x.BackgroundPictureId);
            builder.Property(x => x.BackgroundPictureId);

            builder.Property(x => x.Password)
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property(x => x.LeftSalt)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.RightSalt)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.IsAdmin)
                   .IsRequired();

            builder.Property(x => x.CreationDate)
                   .IsRequired()
                   .HasDefaultValueSql("now()");

            builder.Property(x => x.LastActive)
                   .IsRequired()
                   .HasDefaultValueSql("now()");
        }

        #endregion
    }
}
