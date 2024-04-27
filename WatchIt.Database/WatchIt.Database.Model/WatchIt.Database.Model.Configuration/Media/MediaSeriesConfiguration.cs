using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaSeriesConfiguration : IEntityTypeConfiguration<MediaSeries>
{
    public void Configure(EntityTypeBuilder<MediaSeries> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Media)
               .WithOne()
               .HasForeignKey<MediaSeries>(x => x.Id)
               .IsRequired();
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.Property(x => x.HasEnded)
               .IsRequired()
               .HasDefaultValue(false);
    }
}