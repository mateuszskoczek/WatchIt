using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Accounts;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Configuration.Media;

public class MediumRatingConfiguration : RatingEntityConfiguration<MediumRating>
{
    #region PUBLIC METHODS
    
    public override void Configure(EntityTypeBuilder<MediumRating> builder)
    {
        builder.ToTable("MediumRatings", "media");
        builder.HasKey(x => new { x.AccountId, x.MediumId });
        
        // Medium
        // FK configured in MediumConfiguration
        builder.Property(x => x.MediumId)
               .IsRequired();
        
        // Version
        builder.Property(b => b.Version)
               .IsRowVersion();
        
        // Generic properties
        base.Configure(builder);
    }
    
    #endregion
}