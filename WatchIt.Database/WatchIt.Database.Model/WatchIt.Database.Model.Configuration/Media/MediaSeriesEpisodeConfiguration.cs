using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaSeriesEpisodeConfiguration : IEntityTypeConfiguration<MediaSeriesEpisode>
{
    public void Configure(EntityTypeBuilder<MediaSeriesEpisode> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.MediaSeriesSeason)
               .WithMany(x => x.MediaSeriesEpisodes)
               .HasForeignKey(x => x.MediaSeriesSeasonId)
               .IsRequired();
        builder.Property(x => x.MediaSeriesSeasonId)
               .IsRequired();

        builder.Property(x => x.Number)
               .IsRequired();

        builder.Property(x => x.Name);

        builder.Property(x => x.IsSpecial)
               .IsRequired()
               .HasDefaultValue(false);
    }
}