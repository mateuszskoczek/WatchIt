using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model.Common;

namespace WatchIt.Database.Model.Media
{
    public class MediaGenre : IEntity<MediaGenre>
    {
        #region PROPERTIES

        public long MediaId { get; set; }
        public short GenreId { get; set; }

        #endregion



        #region NAVIGATION

        public Media Media { get; set; }
        public Genre Genre { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<MediaGenre>.Build(EntityTypeBuilder<MediaGenre> builder)
        {
            builder.HasOne(x => x.Media)
                   .WithMany(x => x.MediaGenres)
                   .HasForeignKey(x => x.MediaId)
                   .IsRequired();
            builder.Property(x => x.MediaId)
                   .IsRequired();

            builder.HasOne(x => x.Genre)
                   .WithMany(x => x.MediaGenres)
                   .HasForeignKey(x => x.GenreId)
                   .IsRequired();
            builder.Property(x => x.GenreId)
                   .IsRequired();
        }

        #endregion
    }
}
