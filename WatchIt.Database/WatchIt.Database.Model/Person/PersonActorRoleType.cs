using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.DataSeeding;

namespace WatchIt.Database.Model.Person
{
    public class PersonActorRoleType : IEntity<PersonActorRoleType>
    {
        #region PROPERTIES

        public short Id { get; set; }
        public string Name { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<PersonActorRoleType>.Build(EntityTypeBuilder<PersonActorRoleType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();
        }

        static IEnumerable<PersonActorRoleType> IEntity<PersonActorRoleType>.InsertData() => DataReader.Read<PersonActorRoleType>();

        #endregion
    }
}
