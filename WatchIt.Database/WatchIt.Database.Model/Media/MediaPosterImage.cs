using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Media
{
    public class MediaPosterImage : IEntity<MediaPosterImage>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }
        public DateTime UploadDate { get; set; }

        #endregion



        #region NAVIGATION

        public Media Media { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<MediaPosterImage>.Build(EntityTypeBuilder<MediaPosterImage> builder)
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
