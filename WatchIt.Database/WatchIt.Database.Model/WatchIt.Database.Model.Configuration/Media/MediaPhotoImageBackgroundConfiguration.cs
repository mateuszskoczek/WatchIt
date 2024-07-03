using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaPhotoImageBackgroundConfiguration : IEntityTypeConfiguration<MediaPhotoImageBackground>
{
    public void Configure(EntityTypeBuilder<MediaPhotoImageBackground> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
            .IsUnique();
        builder.Property(x => x.Id)
            .IsRequired();

        builder.Property(x => x.IsUniversalBackground)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(x => x.FirstGradientColor)
            .IsRequired()
            .HasMaxLength(3);

        builder.Property(x => x.SecondGradientColor)
            .IsRequired()
            .HasMaxLength(3);
    }
}