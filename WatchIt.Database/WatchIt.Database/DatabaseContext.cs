using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WatchIt.Database.Model;
using WatchIt.Database.Model.Account;

namespace WatchIt.Database
{
    public class DatabaseContext : DbContext
    {
        #region CONSTRUCTORS

        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #endregion



        #region PROPERTIES

        // Account
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountProfilePicture> AccountProfilePictures { get; set; }

        #endregion



        #region METHODS

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql("name=Default");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Account
            EntityBuilder.Build<Account>(modelBuilder);
            EntityBuilder.Build<AccountProfilePicture>(modelBuilder);
        }

        #endregion
    }
}
