using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.DataSeeding;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Common
{
    public class Genre : IEntity<Genre>
    {
        #region PROPERTIES

        public short Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        #endregion



        #region NAVIGATION

        public IEnumerable<MediaGenre> MediaGenres { get; set; }
        public IEnumerable<Media.Media> Media { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<Genre>.Build(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.Description)
                   .HasMaxLength(1000);

            // Navigation
            builder.HasMany(x => x.Media)
                   .WithMany(x => x.Genres)
                   .UsingEntity<MediaGenre>();
        }

        static IEnumerable<Genre> IEntity<Genre>.InsertData() => DataReader.Read<Genre>();

        #endregion
    }
}
