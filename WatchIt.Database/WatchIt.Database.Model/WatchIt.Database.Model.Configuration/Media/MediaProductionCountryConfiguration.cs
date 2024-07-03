using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaProductionCountryConfiguration : IEntityTypeConfiguration<MediaProductionCountry>
{
    public void Configure(EntityTypeBuilder<MediaProductionCountry> builder)
    {
        builder.HasOne(x => x.Media)
               .WithMany(x => x.MediaProductionCountries)
               .HasForeignKey(x => x.MediaId)
               .IsRequired();
        builder.Property(x => x.MediaId)
               .IsRequired();

        builder.HasOne(x => x.Country)
               .WithMany(x => x.MediaProductionCountries)
               .HasForeignKey(x => x.CountryId)
               .IsRequired();
        builder.Property(x => x.CountryId)
               .IsRequired();
    }
}