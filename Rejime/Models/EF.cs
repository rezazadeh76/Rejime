namespace Rejime.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EF : DbContext
    {
        public EF()
            : base("name=EF")
        {
        }

        #region khodadadi
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
