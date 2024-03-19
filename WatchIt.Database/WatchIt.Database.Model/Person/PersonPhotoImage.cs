using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model.Account;

namespace WatchIt.Database.Model.Person
{
    public class PersonPhotoImage : IEntity<PersonPhotoImage>
    {
        #region PROPERTIES

        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public string MimeType { get; set; }
        public DateTime UploadDate { get; set; }

        #endregion



        #region NAVIGATION

        public Person Person { get; set; }

        #endregion



        #region PUBLIC METHODS

        static void IEntity<PersonPhotoImage>.Build(EntityTypeBuilder<PersonPhotoImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id)
                   .IsUnique();
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Image)
                   .HasMaxLength(-1)
                   .IsRequired();

            builder.Property(x => x.MimeType)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.UploadDate)
                   .IsRequired()
                   .HasDefaultValueSql("now()");
        }

        #endregion
    }
}
