using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.Account
{
    public class AccountRefreshToken : IEntity<AccountRefreshToken>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public long AccountId { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExtendable { get; set; }

        #endregion



        #region NAVIGATION

        public Account Account { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<AccountRefreshToken>.Build(EntityTypeBuilder<AccountRefreshToken> builder)
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

        #endregion
    }
}
