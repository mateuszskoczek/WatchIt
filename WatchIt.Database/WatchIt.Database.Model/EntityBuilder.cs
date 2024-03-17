using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model
{
    public static class EntityBuilder
    {
        #region PUBLIC METHODS

        public static void Build<T>(ModelBuilder builder) where T : class, IEntity<T> => Build<T>(builder.Entity<T>());
        public static void Build<T>(EntityTypeBuilder<T> builder) where T : class, IEntity<T> => T.Build(builder);

        #endregion
    }
}
