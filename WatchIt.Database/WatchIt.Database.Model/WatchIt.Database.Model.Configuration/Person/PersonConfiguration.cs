using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WatchIt.Database.Model.Configuration.Person;

public class PersonConfiguration : IEntityTypeConfiguration<Model.Person.Person>
{
    public void Configure(EntityTypeBuilder<Model.Person.Person> builder)
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

        builder.HasOne(x => x.Gender)
               .WithMany()
               .HasForeignKey(x => x.GenderId);
        builder.Property(x => x.GenderId);

        builder.HasOne(x => x.PersonPhoto)
               .WithOne(x => x.Person)
               .HasForeignKey<Model.Person.Person>(e => e.PersonPhotoId);
        builder.Property(x => x.PersonPhotoId);
    }
}