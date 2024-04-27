using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Account;

namespace WatchIt.Database.Model.Configuration.Account;

public class AccountRefreshTokenConfiguration : IEntityTypeConfiguration<AccountRefreshToken>
{
    public void Configure(EntityTypeBuilder<AccountRefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired();

        builder.HasOne(x => x.Account)
               .WithMany(x => x.AccountRefreshTokens)
               .HasForeignKey(x => x.AccountId)
               .IsRequired();
        builder.Property(x => x.AccountId)
               .IsRequired();

        builder.Property(x => x.ExpirationDate)
               .IsRequired();

        builder.Property(x => x.IsExtendable)
               .IsRequired();
    }
}