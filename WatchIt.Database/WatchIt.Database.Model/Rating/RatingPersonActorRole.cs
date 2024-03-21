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
    public class RatingPersonActorRole : IEntity<RatingPersonActorRole>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public Guid PersonActorRoleId { get; set; }
        public long AccountId { get; set; }
        public short Rating { get; set; }

        #endregion



        #region NAVIGATION

        public PersonActorRole PersonActorRole { get; set; }
        public Account.Account Account { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<RatingPersonActorRole>.Build(EntityTypeBuilder<RatingPersonActorRole> builder)
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
        }

        #endregion
    }
}
