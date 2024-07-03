using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.ViewCount;

namespace WatchIt.Database.Model.Configuration.ViewCount;

public class ViewCountPersonConfiguration : IEntityTypeConfiguration<ViewCountPerson>
{
    public void Configure(EntityTypeBuilder<ViewCountPerson> builder)
    {
        builder.ToTable("ViewCountsPerson");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.Person)
               .WithMany(x => x.ViewCountsPerson)
               .HasForeignKey(x => x.PersonId)
               .IsRequired();
        builder.Property(x => x.PersonId)
               .IsRequired();

        builder.Property(x => x.Date)
               .IsRequired()
               .HasDefaultValueSql("now()");

        builder.Property(x => x.ViewCount)
               .IsRequired()
               .HasDefaultValue(0);
    }
}