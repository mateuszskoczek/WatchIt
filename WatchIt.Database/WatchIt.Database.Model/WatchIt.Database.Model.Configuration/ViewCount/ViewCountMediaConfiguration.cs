using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.ViewCount;

namespace WatchIt.Database.Model.Configuration.ViewCount;

public class ViewCountMediaConfiguration : IEntityTypeConfiguration<ViewCountMedia>
{
    public void Configure(EntityTypeBuilder<ViewCountMedia> builder)
    {
        builder.ToTable("ViewCountsMedia");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.Media)
               .WithMany(x => x.ViewCountsMedia)
               .HasForeignKey(x => x.MediaId)
               .IsRequired();
        builder.Property(x => x.MediaId)
               .IsRequired();

        builder.Property(x => x.Date)
               .IsRequired()
               .HasDefaultValueSql("now()");

        builder.Property(x => x.ViewCount)
               .IsRequired()
               .HasDefaultValue(0);
    }
}