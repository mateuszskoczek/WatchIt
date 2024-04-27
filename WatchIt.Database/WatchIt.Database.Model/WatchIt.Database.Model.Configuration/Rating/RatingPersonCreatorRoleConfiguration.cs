using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Rating;

namespace WatchIt.Database.Model.Configuration.Rating;

public class RatingPersonCreatorRoleConfiguration : IEntityTypeConfiguration<RatingPersonCreatorRole>
{
    public void Configure(EntityTypeBuilder<RatingPersonCreatorRole> builder)
    {
        builder.ToTable("RatingsPersonCreatorRole");

        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.PersonCreatorRole)
               .WithMany(x => x.RatingPersonCreatorRole)
               .HasForeignKey(x => x.PersonCreatorRoleId)
               .IsRequired();
        builder.Property(x => x.PersonCreatorRoleId)
               .IsRequired();

        builder.HasOne(x => x.Account)
               .WithMany(x => x.RatingPersonCreatorRole)
               .HasForeignKey(x => x.AccountId)
               .IsRequired();
        builder.Property(x => x.AccountId)
               .IsRequired();

        builder.Property(x => x.Rating)
               .IsRequired();
    }
}