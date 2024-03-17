using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchIt.Database.Model
{
    public interface IEntity<T> where T : class
    {
        #region METHODS

        static abstract void Build(EntityTypeBuilder<T> builder);
        static virtual IEnumerable<T> InsertData() => Array.Empty<T>();

        #endregion
    }
}
