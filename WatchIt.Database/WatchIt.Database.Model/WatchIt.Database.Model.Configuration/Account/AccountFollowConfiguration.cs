using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Account;

namespace WatchIt.Database.Model.Configuration.Account;

public class AccountFollowConfiguration : IEntityTypeConfiguration<AccountFollow>
{
    public void Configure(EntityTypeBuilder<AccountFollow> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();
        
        builder.HasOne(x => x.AccountFollower)
               .WithMany(x => x.AccountFollows)
               .HasForeignKey(x => x.AccountFollowerId)
               .IsRequired();
        builder.Property(x => x.AccountFollowerId)
               .IsRequired();
        
        builder.HasOne(x => x.AccountFollowed)
               .WithMany(x => x.AccountFollowedBy)
               .HasForeignKey(x => x.AccountFollowedId)
               .IsRequired();
        builder.Property(x => x.AccountFollowedId)
               .IsRequired();
    }
}