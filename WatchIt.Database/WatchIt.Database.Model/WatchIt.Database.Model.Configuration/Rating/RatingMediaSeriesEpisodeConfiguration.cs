using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Configuration.Rating;

public class RatingMediaSeriesEpisodeConfiguration : IEntityTypeConfiguration<RatingMediaSeriesEpisode>
{
    public void Configure(EntityTypeBuilder<RatingMediaSeriesEpisode> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.MediaSeriesEpisode)
               .WithMany(x => x.RatingMediaSeriesEpisode)
               .HasForeignKey(x => x.MediaSeriesEpisodeId)
               .IsRequired();
        builder.Property(x => x.MediaSeriesEpisodeId)
               .IsRequired();

        builder.HasOne(x => x.Account)
               .WithMany(x => x.RatingMediaSeriesEpisode)
               .HasForeignKey(x => x.AccountId)
               .IsRequired();
        builder.Property(x => x.AccountId)
               .IsRequired();

        builder.Property(x => x.Rating)
               .IsRequired();
        
        builder.Property(x => x.Date)
               .IsRequired()
               .HasDefaultValueSql("now()");
    }
}