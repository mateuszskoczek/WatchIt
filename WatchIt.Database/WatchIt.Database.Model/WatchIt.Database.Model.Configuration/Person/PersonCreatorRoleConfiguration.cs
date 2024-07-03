using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Person;

namespace WatchIt.Database.Model.Configuration.Person;

public class PersonCreatorRoleConfiguration : IEntityTypeConfiguration<PersonCreatorRole>
{
    public void Configure(EntityTypeBuilder<PersonCreatorRole> builder)
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
}