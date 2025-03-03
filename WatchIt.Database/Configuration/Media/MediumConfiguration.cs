using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Configuration.Media;

public class MediumConfiguration : IEntityTypeConfiguration<Medium>
{
    #region PUBLIC METHODS

    public void Configure(EntityTypeBuilder<Medium> builder)
    {
        builder.ToTable("Media", "media");
        builder.HasDiscriminator<MediumType>("Type")
               .HasValue<MediumMovie>(MediumType.Movie)
               .HasValue<MediumSeries>(MediumType.Series);
        
        // Id
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired()
               .UseIdentityAlwaysColumn();
        
        // Type
        builder.Property(x => x.Type)
               .IsRequired();
        
        // Title
        builder.Property(x => x.Title)
               .HasMaxLength(250)
               .IsRequired();
        
        // Original title
        builder.Property(x => x.OriginalTitle)
               .HasMaxLength(250);
        
        // Description
        builder.Property(x => x.Description)
               .HasMaxLength(1000);
        
        // Duration
        builder.Property(x => x.Duration);
        
        // Release date
        builder.Property(x => x.ReleaseDate);
        
        // Version
        builder.Property(b => b.Version)
               .IsRowVersion();


        #region Navigation

        // MediumGenre
        builder.HasMany(x => x.Genres)
               .WithMany(x => x.Media)
               .UsingEntity<MediumGenre>(
                   x => x.HasOne(y => y.Genre)
                         .WithMany(y => y.MediaRelationObjects)
                         .HasForeignKey(y => y.GenreId)
                         .IsRequired(),
                   x => x.HasOne(y => y.Medium)
                         .WithMany(y => y.GenresRelationshipObjects)
                         .HasForeignKey(y => y.MediumId)
                         .IsRequired()
               );
        
        // MediumRating
        builder.HasMany(x => x.RatedBy)
               .WithMany(x => x.MediaRated)
               .UsingEntity<MediumRating>(
                   x => x.HasOne(y => y.Account)
                         .WithMany(y => y.MediaRatings)
                         .HasForeignKey(y => y.AccountId)
                         .IsRequired(),
                   x => x.HasOne(y => y.Medium)
                         .WithMany(y => y.Ratings)
                         .HasForeignKey(y => y.MediumId)
                         .IsRequired()
               );

        #endregion
    }

    #endregion
}