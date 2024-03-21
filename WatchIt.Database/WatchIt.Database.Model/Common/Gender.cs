using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.DataSeeding;
using WatchIt.Database.Model.Person;

namespace WatchIt.Database.Model.Common
{
    public class Gender : IEntity<Gender>
    {
        #region PROPERTIES

        public short Id { get; set; }
        public string Name { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<Gender>.Build(EntityTypeBuilder<Gender> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .IsRequired();
        }

        static IEnumerable<Gender> IEntity<Gender>.InsertData() => DataReader.Read<Gender>();

        #endregion
    }
}
