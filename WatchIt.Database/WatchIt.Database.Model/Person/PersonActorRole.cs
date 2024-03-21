using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Person
{
    public class PersonActorRole : IEntity<PersonActorRole>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public long PersonId { get; set; }
        public long MediaId { get; set; }
        public short PersonActorRoleTypeId { get; set; }
        public string RoleName { get; set; }

        #endregion



        #region NAVIGATION

        public Person Person { get; set; }
        public Media.Media Media { get; set; }
        public PersonActorRoleType PersonActorRoleType { get; set; }

        public IEnumerable<RatingPersonActorRole> RatingPersonActorRole { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<PersonActorRole>.Build(EntityTypeBuilder<PersonActorRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.Person)
                   .WithMany(x => x.PersonActorRoles)
                   .HasForeignKey(x => x.PersonId)
                   .IsRequired();
            builder.Property(x => x.PersonId)
                   .IsRequired();

            builder.HasOne(x => x.Media)
                   .WithMany(x => x.PersonActorRoles)
                   .HasForeignKey(x => x.MediaId)
                   .IsRequired();
            builder.Property(x => x.MediaId)
                   .IsRequired();

            builder.HasOne(x => x.PersonActorRoleType)
                   .WithMany()
                   .HasForeignKey(x => x.PersonActorRoleTypeId)
                   .IsRequired();
            builder.Property(x => x.PersonActorRoleTypeId)
                   .IsRequired();
        }

        #endregion
    }
}
