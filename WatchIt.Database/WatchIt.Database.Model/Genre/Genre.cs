using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Genre
{
    public class Genre : IEntity<Genre>
    {
        #region PROPERTIES

        public short Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

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
        }

        static IEnumerable<Genre> IEntity<Genre>.InsertData() => new List<Genre>
        {
            new Genre { Id = 1, Name = "Comedy" },
            new Genre { Id = 2, Name = "Thriller" },
            new Genre { Id = 3, Name = "Horror" },
        };

        #endregion
    }
}
