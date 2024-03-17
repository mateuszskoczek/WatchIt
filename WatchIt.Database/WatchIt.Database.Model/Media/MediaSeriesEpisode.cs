using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Media
{
    public class MediaSeriesEpisode : IEntity<MediaSeriesEpisode>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public Guid MediaSeriesSeasonId { get; set; }
        public short Number { get; set; }
        public string? Name { get; set; }
        public bool IsSpecial { get; set; }

        #endregion



        #region NAVIGATION

        public MediaSeriesSeason MediaSeriesSeason { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<MediaSeriesEpisode>.Build(EntityTypeBuilder<MediaSeriesEpisode> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.MediaSeriesSeason)
                   .WithMany(x => x.MediaSeriesEpisodes)
                   .HasForeignKey(x => x.MediaSeriesSeasonId)
                   .IsRequired();
            builder.Property(x => x.MediaSeriesSeasonId)
                   .IsRequired();

            builder.Property(x => x.Number)
                   .IsRequired();

            builder.Property(x => x.Name);

            builder.Property(x => x.IsSpecial)
                   .IsRequired()
                   .HasDefaultValue(false);
        }

        #endregion
    }
}
