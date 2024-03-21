using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Rating
{
    public class RatingMedia : IEntity<RatingMedia>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public long MediaId { get; set; }
        public long AccountId { get; set; }
        public short Rating { get; set; }

        #endregion



        #region NAVIGATION

        public Media.Media Media { get; set; }
        public Account.Account Account { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<RatingMedia>.Build(EntityTypeBuilder<RatingMedia> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.Media)
                   .WithMany(x => x.RatingMedia)
                   .HasForeignKey(x => x.MediaId)
                   .IsRequired();
            builder.Property(x => x.MediaId)
                   .IsRequired();

            builder.HasOne(x => x.Account)
                   .WithMany(x => x.RatingMedia)
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
