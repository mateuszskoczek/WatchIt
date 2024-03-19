using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model.Common;

namespace WatchIt.Database.Model.Media
{
    public class MediaProductionCountry : IEntity<MediaProductionCountry>
    {
        #region PROPERTIES

        public long MediaId { get; set; }
        public short CountryId { get; set; }

        #endregion



        #region NAVIGATION

        public Media Media { get; set; }
        public Country Country { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<MediaProductionCountry>.Build(EntityTypeBuilder<MediaProductionCountry> builder)
        {
            builder.HasOne(x => x.Media)
                   .WithMany(x => x.MediaProductionCountries)
                   .HasForeignKey(x => x.MediaId)
                   .IsRequired();
            builder.Property(x => x.MediaId)
                   .IsRequired();

            builder.HasOne(x => x.Country)
                   .WithMany(x => x.MediaProductionCountries)
                   .HasForeignKey(x => x.CountryId)
                   .IsRequired();
            builder.Property(x => x.CountryId)
                   .IsRequired();
        }

        #endregion
    }
}
