using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Roles;

namespace WatchIt.Database.Configuration.Roles;

public class RoleRatingConfiguration : RatingEntityConfiguration<RoleRating>
{
    #region PUBLIC METHODS
    
    public override void Configure(EntityTypeBuilder<RoleRating> builder)
    {
        builder.ToTable("RoleRatings", "roles");
        builder.HasKey(x => new { x.AccountId, x.RoleId });
        
        // Role
        // FK configured in RoleConfiguration
        builder.Property(x => x.RoleId)
               .IsRequired();
        
        // Version
        builder.Property(b => b.Version)
               .IsRowVersion();
        
        // Generic properties
        base.Configure(builder);
    }
    
    #endregion
}