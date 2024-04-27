using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaSeriesSeasonConfiguration : IEntityTypeConfiguration<MediaSeriesSeason>
{
    public void Configure(EntityTypeBuilder<MediaSeriesSeason> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.MediaSeries)
               .WithMany(x => x.MediaSeriesSeasons)
               .HasForeignKey(x => x.MediaSeriesId)
               .IsRequired();
        builder.Property(x => x.MediaSeriesId)
               .IsRequired();

        builder.Property(x => x.Number)
               .IsRequired();

        builder.Property(x => x.Name);
    }
}