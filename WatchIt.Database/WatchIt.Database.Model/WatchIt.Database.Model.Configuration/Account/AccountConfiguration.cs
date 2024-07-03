using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WatchIt.Database.Model.Configuration.Account;

public class AccountConfiguration : IEntityTypeConfiguration<Model.Account.Account>
{
    public void Configure(EntityTypeBuilder<Model.Account.Account> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.Property(x => x.Username)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.Email)
               .HasMaxLength(320)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasMaxLength(1000);

        builder.HasOne(x => x.Gender)
               .WithMany()
               .HasForeignKey(x => x.GenderId);
        builder.Property(x => x.GenderId);

        builder.HasOne(x => x.ProfilePicture)
               .WithOne(x => x.Account)
               .HasForeignKey<Model.Account.Account>(e => e.ProfilePictureId);
        builder.Property(x => x.ProfilePictureId);

        builder.HasOne(x => x.BackgroundPicture)
               .WithMany()
               .HasForeignKey(x => x.BackgroundPictureId);
        builder.Property(x => x.BackgroundPictureId);

        builder.Property(x => x.Password)
               .HasMaxLength(1000)
               .IsRequired();

        builder.Property(x => x.LeftSalt)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(x => x.RightSalt)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(x => x.IsAdmin)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(x => x.CreationDate)
               .IsRequired()
               .HasDefaultValueSql("now()");

        builder.Property(x => x.LastActive)
               .IsRequired()
               .HasDefaultValueSql("now()");
    }
}