using System.Collections.Concurrent;
using TinyLedger.Domain;

namespace TinyLedger.Infrastructure;

public class InMemoryLedgerRepository : ILedgerRepository
{
    private readonly ConcurrentDictionary<string, ConcurrentQueue<Transaction>> _transactions = new();
    private readonly ConcurrentDictionary<string, decimal> _balances = new();

    public Task AddTransaction(string accountId, Transaction transaction)
    {
        var queue = _transactions.GetOrAdd(accountId, _ => new ConcurrentQueue<Transaction>());
        queue.Enqueue(transaction);

        _balances.AddOrUpdate(accountId,
            transaction.Type == TransactionType.Deposit ? transaction.Amount : -transaction.Amount,
            (_, currentBalance) => currentBalance + (transaction.Type == TransactionType.Deposit ? transaction.Amount : -transaction.Amount)
        );

        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Transaction>> GetTransactionHistory(string accountId)
    {
        if (_transactions.TryGetValue(accountId, out var queue))
            return Task.FromResult<IReadOnlyList<Transaction>>(queue.ToList());

        return Task.FromResult<IReadOnlyList<Transaction>>(new List<Transaction>());
    }

    public Task<decimal> GetBalance(string accountId)
    {
        _balances.TryGetValue(accountId, out var balance);
        return Task.FromResult(balance);
    }
}
