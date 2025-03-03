using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Genders;

namespace WatchIt.Database.Configuration.Genders;

public class GenderConfiguration : IEntityTypeConfiguration<Gender>
{
    #region PUBLIC METHODS

    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.ToTable("Genders", "genders");
        
        // Id
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired()
               .UseIdentityAlwaysColumn();
        
        // Name
        builder.Property(x => x.Name)
               .HasMaxLength(100)
               .IsRequired();
        
        // Version
        builder.Property(b => b.Version)
               .IsRowVersion();
    }

    #endregion
}