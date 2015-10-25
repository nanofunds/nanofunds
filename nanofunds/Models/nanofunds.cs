namespace nanofunds.Models
{
    using System.Data.Entity;

    public class nanofunds : DbContext
    {
        public nanofunds()
            : base("name=nanofunds")
        {
        }

        public virtual DbSet<Ledger> Ledgers { get; set; }

        public virtual DbSet<Merchant> Merchants { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Merchant>().HasMany(x => x.Receipts);
            modelBuilder.Entity<Merchant>().HasMany(x => x.Predictions);

            modelBuilder.Entity<Ledger>().HasMany(x => x.Accounts);
        }
    }
}