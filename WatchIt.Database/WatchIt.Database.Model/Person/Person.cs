using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model.Account;

namespace WatchIt.Database.Model.Person
{
    public class Person : IEntity<Person>
    {
        #region PROPERTIES

        public long Id { get; set; }
        public string Name { get; set; }
        public string? FullName { get; set; }
        public string? Description { get; set; }
        public DateOnly? BirthDate { get; set; }
        public DateOnly? DeathDate { get; set; }
        public Guid? PersonPhotoId { get; set; }

        #endregion



        #region NAVIGATION

        public PersonPhotoImage? PersonPhoto { get; set; }
        public IEnumerable<PersonActorRole> PersonActorRoles { get; set; }
        public IEnumerable<PersonCreatorRole> PersonCreatorRoles { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<Person>.Build(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.FullName)
                   .HasMaxLength(200);

            builder.Property(x => x.Description)
                   .HasMaxLength(1000);

            builder.Property(x => x.BirthDate);

            builder.Property(x => x.DeathDate);

            builder.HasOne(x => x.PersonPhoto)
                   .WithOne(x => x.Person)
                   .HasForeignKey<Person>(e => e.PersonPhotoId);
            builder.Property(x => x.PersonPhotoId);
        }

        #endregion
    }
}
