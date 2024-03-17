using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Account
{
    public class AccountProfilePicture : IEntity<AccountProfilePicture>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }
        public DateTimeOffset UploadDate { get; set; }

        #endregion



        #region NAVIGATION

        public Account Account { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<AccountProfilePicture>.Build(EntityTypeBuilder<AccountProfilePicture> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Image)
                   .HasMaxLength(-1)
                   .IsRequired();

            builder.Property(x => x.MimeType)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.UploadDate)
                   .IsRequired()
                   .HasDefaultValueSql("now()");
        }

        #endregion
    }
}
