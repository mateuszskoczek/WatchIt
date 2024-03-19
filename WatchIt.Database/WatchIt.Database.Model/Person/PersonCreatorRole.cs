using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Person
{
    public class PersonCreatorRole : IEntity<PersonCreatorRole>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public long PersonId { get; set; }
        public long MediaId { get; set; }
        public short PersonCreatorRoleTypeId { get; set; }

        #endregion



        #region NAVIGATION

        public Person Person { get; set; }
        public Media.Media Media { get; set; }
        public PersonCreatorRoleType PersonCreatorRoleType { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<PersonCreatorRole>.Build(EntityTypeBuilder<PersonCreatorRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.Person)
                   .WithMany(x => x.PersonCreatorRoles)
                   .HasForeignKey(x => x.PersonId)
                   .IsRequired();
            builder.Property(x => x.PersonId)
                   .IsRequired();

            builder.HasOne(x => x.Media)
                   .WithMany(x => x.PersonCreatorRoles)
                   .HasForeignKey(x => x.MediaId)
                   .IsRequired();
            builder.Property(x => x.MediaId)
                   .IsRequired();

            builder.HasOne(x => x.PersonCreatorRoleType)
                   .WithMany()
                   .HasForeignKey(x => x.PersonCreatorRoleTypeId)
                   .IsRequired();
            builder.Property(x => x.PersonCreatorRoleTypeId)
                   .IsRequired();
        }

        #endregion
    }
}
