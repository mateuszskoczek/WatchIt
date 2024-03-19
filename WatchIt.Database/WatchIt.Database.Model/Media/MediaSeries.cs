using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Media
{
    public class MediaSeries : IEntity<MediaSeries>
    {
        #region PROPERTIES

        public long Id { get; set; }
        public bool HasEnded { get; set; }

        #endregion



        #region NAVIGATION

        public Media Media { get; set; }
        public IEnumerable<MediaSeriesSeason> MediaSeriesSeasons { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<MediaSeries>.Build(EntityTypeBuilder<MediaSeries> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Media)
                   .WithOne()
                   .HasForeignKey<Media>(x => x.Id)
                   .IsRequired();
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.HasEnded);
        }

        #endregion
    }
}
