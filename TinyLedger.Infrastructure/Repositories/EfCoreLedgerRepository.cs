using Microsoft.EntityFrameworkCore;
using TinyLedger.Domain;
using TinyLedger.Infrastructure.Persistence;

namespace TinyLedger.Infrastructure.Repositories;

public class EfCoreLedgerRepository : ILedgerRepository
{
    private readonly TinyLedgerDbContext _context;

    public EfCoreLedgerRepository(TinyLedgerDbContext context)
    {
        _context = context;
    }

    public async Task AddTransaction(string accountId, Transaction transaction)
    {
        transaction.AccountId = accountId;
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Transaction>> GetTransactionHistory(string accountId)
    {
        return await _context.Transactions
            .Where(t => t.AccountId == accountId)
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync();
    }

    public async Task<decimal> GetBalance(string accountId)
    {
        var transactions = await _context.Transactions
            .Where(t => t.AccountId == accountId)
            .ToListAsync();

        return transactions.Sum(t =>
            t.Type == TransactionType.Deposit ? t.Amount : -t.Amount);
    }

    public async Task CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}