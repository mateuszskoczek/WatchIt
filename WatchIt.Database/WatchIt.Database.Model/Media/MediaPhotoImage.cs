using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Media
{
    public class MediaPhotoImage : IEntity<MediaPhotoImage>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public long MediaId { get; set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsMediaBackground { get; set; }
        public bool IsUniversalBackground { get; set; }

        #endregion



        #region NAVIGATION

        public Media Media { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<MediaPhotoImage>.Build(EntityTypeBuilder<MediaPhotoImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.Media)
                   .WithMany(x => x.MediaPhotoImages)
                   .HasForeignKey(x => x.MediaId)
                   .IsRequired();
            builder.Property(x => x.MediaId)
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

            builder.Property(x => x.IsMediaBackground)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(x => x.IsUniversalBackground)
                   .IsRequired()
                   .HasDefaultValue(false);
        }

        #endregion
    }
}
