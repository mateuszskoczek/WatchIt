using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Person;

namespace WatchIt.Database.Model.Rating
{
    public class RatingMediaSeriesSeason : IEntity<RatingMediaSeriesSeason>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public Guid MediaSeriesSeasonId { get; set; }
        public long AccountId { get; set; }
        public short Rating { get; set; }

        #endregion



        #region NAVIGATION

        public MediaSeriesSeason MediaSeriesSeason { get; set; }
        public Account.Account Account { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<RatingMediaSeriesSeason>.Build(EntityTypeBuilder<RatingMediaSeriesSeason> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.MediaSeriesSeason)
                   .WithMany(x => x.RatingMediaSeriesSeason)
                   .HasForeignKey(x => x.MediaSeriesSeasonId)
                   .IsRequired();
            builder.Property(x => x.MediaSeriesSeasonId)
                   .IsRequired();

            builder.HasOne(x => x.Account)
                   .WithMany(x => x.RatingMediaSeriesSeason)
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
