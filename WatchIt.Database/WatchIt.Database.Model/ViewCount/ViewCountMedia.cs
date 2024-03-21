using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model.ViewCount
{
    public class ViewCountMedia : IEntity<ViewCountMedia>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public long MediaId { get; set; }
        public DateOnly Date { get; set; }
        public long ViewCount { get; set; }

        #endregion



        #region NAVIGATION

        public Media.Media Media { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<ViewCountMedia>.Build(EntityTypeBuilder<ViewCountMedia> builder)
        {
            builder.ToTable("ViewCountsMedia");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.HasOne(x => x.Media)
                   .WithMany(x => x.ViewCountsMedia)
                   .HasForeignKey(x => x.MediaId)
                   .IsRequired();
            builder.Property(x => x.MediaId)
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
