using TinyLedger.Domain;
using System.Collections.Concurrent;

namespace TinyLedger.Infrastructure;

public class InMemoryLedgerRepository : ILedgerRepository
{
    private readonly ConcurrentDictionary<string, List<Transaction>> _ledger = new();

    public void AddTransaction(Transaction transaction)
    {
        // Default to single-user mode if no AccountId logic is required
        AddTransaction("default", transaction);
    }

    public void AddTransaction(string accountId, Transaction transaction)
    {
        var transactions = _ledger.GetOrAdd(accountId, _ => new List<Transaction>());
        lock (transactions) // ensure thread safety for the list
        {
            transactions.Add(transaction);
        }
    }

    public IReadOnlyList<Transaction> GetTransactions()
    {
        return GetTransactionHistory("default").Result;
    }

    public Task<IReadOnlyList<Transaction>> GetTransactionHistory(string accountId)
    {
        if (_ledger.TryGetValue(accountId, out var list))
        {
            lock (list)
            {
                return Task.FromResult<IReadOnlyList<Transaction>>(list.ToList().AsReadOnly());
            }
        }

        return Task.FromResult<IReadOnlyList<Transaction>>(new List<Transaction>().AsReadOnly());
    }

    public Task<decimal> GetBalance(string accountId)
    {
        return Task.FromResult(
            _ledger.TryGetValue(accountId, out var list)
                ? list.Sum(t => t.GetSignedAmount())
                : 0m
        );
    }
}