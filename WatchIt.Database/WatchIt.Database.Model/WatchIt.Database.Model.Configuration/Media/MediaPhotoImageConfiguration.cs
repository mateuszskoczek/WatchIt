using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaPhotoImageConfiguration : IEntityTypeConfiguration<MediaPhotoImage>
{
    public void Configure(EntityTypeBuilder<MediaPhotoImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.Media)
               .WithMany(x => x.MediaPhotoImages)
               .HasForeignKey(x => x.MediaId)
               .IsRequired();
        builder.Property(x => x.MediaId)
               .IsRequired();

        builder.Property(x => x.Image)
               .HasMaxLength(-1)
               .IsRequired();

        builder.Property(x => x.MimeType)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.UploadDate)
               .IsRequired()
               .HasDefaultValueSql("now()");

        builder.Property(x => x.IsMediaBackground)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(x => x.IsUniversalBackground)
               .IsRequired()
               .HasDefaultValue(false);
    }
}