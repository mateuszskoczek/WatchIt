using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.DataSeeding;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Common
{
    public class Country : IEntity<Country>
    {
        #region PROPERTIES

        public short Id { get; set; }
        public string Name { get; set; }

        #endregion



        #region NAVIGATION

        public IEnumerable<MediaProductionCountry> MediaProductionCountries { get; set; }
        public IEnumerable<Media.Media> MediaProduction { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<Country>.Build(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            // Navigation
            builder.HasMany(x => x.MediaProduction)
                   .WithMany(x => x.ProductionCountries)
                   .UsingEntity<MediaProductionCountry>();
        }

        static IEnumerable<Country> IEntity<Country>.InsertData() => DataReader.Read<Country>();

        #endregion
    }
}
