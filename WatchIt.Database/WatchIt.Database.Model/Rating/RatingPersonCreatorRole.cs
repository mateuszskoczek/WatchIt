using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model.Person;

namespace WatchIt.Database.Model.Rating
{
    public class RatingPersonCreatorRole : IEntity<RatingPersonCreatorRole>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public Guid PersonCreatorRoleId { get; set; }
        public long AccountId { get; set; }
        public short Rating { get; set; }

        #endregion



        #region NAVIGATION

        public PersonCreatorRole PersonCreatorRole { get; set; }
        public Account.Account Account { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<RatingPersonCreatorRole>.Build(EntityTypeBuilder<RatingPersonCreatorRole> builder)
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

        #endregion
    }
}
