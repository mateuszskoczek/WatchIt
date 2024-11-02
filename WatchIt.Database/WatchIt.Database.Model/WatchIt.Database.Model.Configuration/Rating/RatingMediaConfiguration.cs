using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Configuration.Rating;

public class RatingMediaConfiguration : IEntityTypeConfiguration<RatingMedia>
{
    public void Configure(EntityTypeBuilder<RatingMedia> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.Media)
               .WithMany(x => x.RatingMedia)
               .HasForeignKey(x => x.MediaId)
               .IsRequired();
        builder.Property(x => x.MediaId)
               .IsRequired();

        builder.HasOne(x => x.Account)
               .WithMany(x => x.RatingMedia)
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