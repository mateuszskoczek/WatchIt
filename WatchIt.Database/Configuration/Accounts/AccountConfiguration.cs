using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchIt.Database.Model.Accounts;

namespace WatchIt.Database.Configuration.Accounts;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    #region PUBLIC METHODS
    
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts", "accounts");
        
        // Id
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id)
               .IsUnique();
        builder.Property(x => x.Id)
               .IsRequired()
               .UseIdentityAlwaysColumn();

        // Username
        builder.Property(x => x.Username)
               .HasMaxLength(50)
               .IsRequired();

        // Email
        builder.Property(x => x.Email)
               .HasMaxLength(320)
               .IsRequired();

        // Password
        builder.Property(x => x.Password)
               .HasMaxLength(1000)
               .IsRequired();
        
        // Left salt
        builder.Property(x => x.LeftSalt)
               .HasMaxLength(20)
               .IsRequired();
        
        // Right salt
        builder.Property(x => x.RightSalt)
               .HasMaxLength(20)
               .IsRequired();
        
        // Is admin
        builder.Property(x => x.IsAdmin)
               .IsRequired()
               .HasDefaultValue(false);
        
        // Join date
        builder.Property(x => x.JoinDate)
               .IsRequired()
               .HasDefaultValueSql("now()");
        
        // Active date
        builder.Property(x => x.ActiveDate)
               .IsRequired()
               .HasDefaultValueSql("now()");

        // Description
        builder.Property(x => x.Description)
               .HasMaxLength(1000);
        
        // Gender
        builder.HasOne(x => x.Gender)
               .WithMany(x => x.Accounts)
               .HasForeignKey(x => x.GenderId);
        builder.Property(x => x.GenderId);
        
        // Version
        builder.Property(b => b.Version)
               .IsRowVersion();


        #region Navigation

        // AccountFollow
        builder.HasMany(x => x.Follows)
               .WithMany(x => x.Followers)
               .UsingEntity<AccountFollow>(
                   x => x.HasOne<Account>(y => y.Followed)
                         .WithMany(y => y.FollowersRelationshipObjects)
                         .HasForeignKey(y => y.FollowedId)
                         .IsRequired(),
                   x => x.HasOne<Account>(y => y.Follower)
                         .WithMany(y => y.FollowsRelationshipObjects)
                         .HasForeignKey(y => y.FollowerId)
                         .IsRequired()
               );

        #endregion
    }
    
    #endregion
}