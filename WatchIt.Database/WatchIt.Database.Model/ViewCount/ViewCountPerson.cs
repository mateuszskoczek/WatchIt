using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.ViewCount
{
    public class ViewCountPerson : IEntity<ViewCountPerson>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public long PersonId { get; set; }
        public DateOnly Date { get; set; }
        public long ViewCount { get; set; }

        #endregion



        #region NAVIGATION

        public Person.Person Person { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<ViewCountPerson>.Build(EntityTypeBuilder<ViewCountPerson> builder)
        {
            builder.ToTable("ViewCountsPerson");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.Person)
                   .WithMany(x => x.ViewCountsPerson)
                   .HasForeignKey(x => x.PersonId)
                   .IsRequired();
            builder.Property(x => x.PersonId)
                   .IsRequired();

            builder.Property(x => x.Date)
                   .IsRequired()
                   .HasDefaultValueSql("now()");

            builder.Property(x => x.ViewCount)
                   .IsRequired()
                   .HasDefaultValue(0);
        }

        #endregion
    }
}
