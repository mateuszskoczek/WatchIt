using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Configuration.Rating;

public class RatingPersonActorRoleConfiguration : IEntityTypeConfiguration<RatingPersonActorRole>
{
    public void Configure(EntityTypeBuilder<RatingPersonActorRole> builder)
    {
        builder.ToTable("RatingsPersonActorRole");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.PersonActorRole)
               .WithMany(x => x.RatingPersonActorRole)
               .HasForeignKey(x => x.PersonActorRoleId)
               .IsRequired();
        builder.Property(x => x.PersonActorRoleId)
               .IsRequired();

        builder.HasOne(x => x.Account)
               .WithMany(x => x.RatingPersonActorRole)
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