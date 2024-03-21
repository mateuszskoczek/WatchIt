using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Rating
{
    public class RatingMediaSeriesEpisode : IEntity<RatingMediaSeriesEpisode>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public Guid MediaSeriesEpisodeId { get; set; }
        public long AccountId { get; set; }
        public short Rating { get; set; }

        #endregion



        #region NAVIGATION

        public MediaSeriesEpisode MediaSeriesEpisode { get; set; }
        public Account.Account Account { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<RatingMediaSeriesEpisode>.Build(EntityTypeBuilder<RatingMediaSeriesEpisode> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.MediaSeriesEpisode)
                   .WithMany(x => x.RatingMediaSeriesEpisode)
                   .HasForeignKey(x => x.MediaSeriesEpisodeId)
                   .IsRequired();
            builder.Property(x => x.MediaSeriesEpisodeId)
                   .IsRequired();

            builder.HasOne(x => x.Account)
                   .WithMany(x => x.RatingMediaSeriesEpisode)
                   .HasForeignKey(x => x.AccountId)
                   .IsRequired();
            builder.Property(x => x.AccountId)
                   .IsRequired();

            builder.Property(x => x.Rating)
                   .IsRequired();
        }

        #endregion
    }
}
