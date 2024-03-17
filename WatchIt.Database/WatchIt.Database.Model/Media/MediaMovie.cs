using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Media
{
    public class MediaMovie : IEntity<MediaMovie>
    {
        #region PROPERTIES

        public long Id { get; set; }
        public decimal? Budget { get; set; }

        #endregion



        #region NAVIGATION

        public Media Media { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<MediaMovie>.Build(EntityTypeBuilder<MediaMovie> builder)
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

            builder.Property(x => x.Budget)
                   .HasColumnType("money");
        }

        #endregion
    }
}
