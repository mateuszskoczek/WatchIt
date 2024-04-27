using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Configuration.Rating;

public class RatingMediaSeriesSeasonConfiguration : IEntityTypeConfiguration<RatingMediaSeriesSeason>
{
    public void Configure(EntityTypeBuilder<RatingMediaSeriesSeason> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.MediaSeriesSeason)
               .WithMany(x => x.RatingMediaSeriesSeason)
               .HasForeignKey(x => x.MediaSeriesSeasonId)
               .IsRequired();
        builder.Property(x => x.MediaSeriesSeasonId)
               .IsRequired();

        builder.HasOne(x => x.Account)
               .WithMany(x => x.RatingMediaSeriesSeason)
               .HasForeignKey(x => x.AccountId)
               .IsRequired();
        builder.Property(x => x.AccountId)
               .IsRequired();

        builder.Property(x => x.Rating)
               .IsRequired();
    }
}