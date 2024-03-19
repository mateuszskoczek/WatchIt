using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Media
{
    public class MediaSeriesSeason : IEntity<MediaSeriesSeason>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public long MediaSeriesId { get; set; }
        public short Number { get; set; }
        public string? Name { get; set; }

        #endregion



        #region NAVIGATION

        public MediaSeries MediaSeries { get; set; }
        public IEnumerable<MediaSeriesEpisode> MediaSeriesEpisodes { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<MediaSeriesSeason>.Build(EntityTypeBuilder<MediaSeriesSeason> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.MediaSeries)
                   .WithMany(x => x.MediaSeriesSeasons)
                   .HasForeignKey(x => x.MediaSeriesId)
                   .IsRequired();
            builder.Property(x => x.MediaSeriesId)
                   .IsRequired();

            builder.Property(x => x.Number)
                   .IsRequired();

            builder.Property(x => x.Name);
        }

        #endregion
    }
}
