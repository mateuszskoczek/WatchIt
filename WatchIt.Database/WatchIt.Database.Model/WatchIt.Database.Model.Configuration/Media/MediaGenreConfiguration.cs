using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaGenreConfiguration : IEntityTypeConfiguration<MediaGenre>
{
    public void Configure(EntityTypeBuilder<MediaGenre> builder)
    {
        builder.HasOne(x => x.Media)
               .WithMany(x => x.MediaGenres)
               .HasForeignKey(x => x.MediaId)
               .IsRequired();
        builder.Property(x => x.MediaId)
               .IsRequired();

        builder.HasOne(x => x.Genre)
               .WithMany(x => x.MediaGenres)
               .HasForeignKey(x => x.GenreId)
               .IsRequired();
        builder.Property(x => x.GenreId)
               .IsRequired();
    }
}