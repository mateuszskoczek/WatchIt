using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.People;

namespace WatchIt.Database.Configuration.People;

public class PersonViewCountConfiguration : ViewCountEntityConfiguration<PersonViewCount>
{
    #region PUBLIC METHODS

    public override void Configure(EntityTypeBuilder<PersonViewCount> builder)
    {
        builder.ToTable("PersonViewCounts", "people");
        builder.HasKey(x => new { x.PersonId, x.Date });
        
        // Medium
        builder.HasOne(x => x.Person)
               .WithMany(x => x.ViewCounts)
               .HasForeignKey(x => x.PersonId)
               .IsRequired();
        builder.Property(x => x.PersonId)
               .IsRequired();
        
        // Version
        builder.Property(b => b.Version)
               .IsRowVersion();
        
        // Generic properties
        base.Configure(builder);
    }

    #endregion
}