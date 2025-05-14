using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TinyLedger.Infrastructure.Persistence;

namespace TinyLedger.Infrastructure.Persistence;

public class TinyLedgerDbContextFactory : IDesignTimeDbContextFactory<TinyLedgerDbContext>
{
    public TinyLedgerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TinyLedgerDbContext>();

        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=TinyLedgerDb;User Id=SA;Password=Str0ngP@ss123;Encrypt=false");

        return new TinyLedgerDbContext(optionsBuilder.Options);
    }
}
