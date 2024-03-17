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

namespace WatchIt.Database.Model.Account
{
    public class Account : IEntity<Account>
    {
        #region PROPERTIES

        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public Guid AccountProfilePictureId { get; set; }

        // BackgroundPicture key to MovieImages
        // public Guid BackgroundPicture { get; set; }

        public byte[] Password { get; set; }
        public string LeftSalt { get; set; }
        public string RightSalt { get; set; }
        public bool IsAdmin { get; set; } = false;
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset LastActive { get; set; }

        #endregion



        #region NAVIGATION

        public AccountProfilePicture AccountProfilePicture { get; set; }

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

            builder.HasOne(x => x.AccountProfilePicture)
                   .WithOne(x => x.Account)
                   .HasForeignKey<Account>(e => e.AccountProfilePictureId);
            builder.Property(x => x.AccountProfilePictureId);

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
