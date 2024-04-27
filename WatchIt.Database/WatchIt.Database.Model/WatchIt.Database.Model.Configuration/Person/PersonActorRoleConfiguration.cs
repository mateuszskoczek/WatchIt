using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Person;

namespace WatchIt.Database.Model.Configuration.Person;

public class PersonActorRoleConfiguration : IEntityTypeConfiguration<PersonActorRole>
{
    public void Configure(EntityTypeBuilder<PersonActorRole> builder)
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
}