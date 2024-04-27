using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaMovieConfiguration : IEntityTypeConfiguration<MediaMovie>
{
    public void Configure(EntityTypeBuilder<MediaMovie> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Media)
               .WithOne()
               .HasForeignKey<MediaMovie>(x => x.Id)
               .IsRequired();
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.Property(x => x.Budget)
               .HasColumnType("money");
    }
}