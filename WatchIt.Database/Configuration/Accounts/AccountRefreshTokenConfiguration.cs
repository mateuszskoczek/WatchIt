using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Accounts;

namespace WatchIt.Database.Configuration.Accounts;

public class AccountRefreshTokenConfiguration : IEntityTypeConfiguration<AccountRefreshToken>
{
    #region PUBLIC METHODS

    public void Configure(EntityTypeBuilder<AccountRefreshToken> builder)
    {
        builder.ToTable("AccountRefreshTokens", "accounts");
        
        // Id
        builder.HasKey(x => x.Token);
        builder.HasIndex(x => x.Token)
               .IsUnique();
        builder.Property(x => x.Token)
               .IsRequired();
        
        // Account
        builder.HasOne(x => x.Account)
               .WithMany(x => x.RefreshTokens)
               .HasForeignKey(x => x.AccountId)
               .IsRequired();
        builder.Property(x => x.AccountId)
               .IsRequired();
        
        // Expiration date
        builder.Property(x => x.ExpirationDate)
               .IsRequired();
        
        // Is extendable
        builder.Property(x => x.IsExtendable)
               .IsRequired();
        
        // Version
        builder.Property(b => b.Version)
               .IsRowVersion();
    }

    #endregion
}