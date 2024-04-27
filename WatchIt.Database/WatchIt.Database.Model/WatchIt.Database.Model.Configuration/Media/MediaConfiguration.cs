using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaConfiguration : IEntityTypeConfiguration<Model.Media.Media>
{
    public void Configure(EntityTypeBuilder<Model.Media.Media> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.Property(x => x.Title)
               .HasMaxLength(250)
               .IsRequired();

        builder.Property(x => x.OriginalTitle)
               .HasMaxLength(250);

        builder.Property(x => x.Description)
               .HasMaxLength(1000);

        builder.Property(x => x.ReleaseDate);

        builder.Property(x => x.Length);

        builder.HasOne(x => x.MediaPosterImage)
               .WithOne(x => x.Media)
               .HasForeignKey<Model.Media.Media>(x => x.MediaPosterImageId);
        builder.Property(x => x.MediaPosterImageId);
        
        // Navigation
        builder.HasMany(x => x.Genres)
               .WithMany(x => x.Media)
               .UsingEntity<MediaGenre>();
        builder.HasMany(x => x.ProductionCountries)
               .WithMany(x => x.MediaProduction)
               .UsingEntity<MediaProductionCountry>();
    }
}