using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Common;
using WatchIt.Database.Model.Media;
using WatchIt.Database.Model.Seeding;

namespace WatchIt.Database.Model.Configuration.Common;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();
        
        builder.Property(x => x.Name)
               .HasMaxLength(100)
               .IsRequired();

        // Navigation
        builder.HasMany(x => x.Media)
               .WithMany(x => x.Genres)
               .UsingEntity<MediaGenre>();
        
        // Data
        builder.HasData(DataReader.Read<Genre>());
    }
}    