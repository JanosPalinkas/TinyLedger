using Microsoft.EntityFrameworkCore;
using TinyLedger.Domain;
using TinyLedger.Infrastructure.Configurations;
using TinyLedger.Infrastructure.Persistence.Configurations;

namespace TinyLedger.Infrastructure.Persistence;

public class TinyLedgerDbContext : DbContext
{
    public TinyLedgerDbContext(DbContextOptions<TinyLedgerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Transaction> Transactions { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}