﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Media;

namespace WatchIt.Database.Model.Configuration.Media;

public class MediaPosterImageConfiguration : IEntityTypeConfiguration<MediaPosterImage>
{
    public void Configure(EntityTypeBuilder<MediaPosterImage> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
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
    }
}