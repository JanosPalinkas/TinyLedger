using TinyLedger.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyLedger.Infrastructure
{
    public class InMemoryLedgerRepository : ILedgerRepository
    {
        private readonly Dictionary<string, List<Transaction>> _store = new();
        private readonly object _lock = new();

        public void AddTransaction(string accountId, Transaction transaction)
        {
            lock (_lock)
            {
                if (!_store.ContainsKey(accountId))
                {
                    _store[accountId] = new List<Transaction>();
                }

                _store[accountId].Add(transaction);
            }
        }

        public Task<decimal> GetBalance(string accountId)
        {
            lock (_lock)
            {
                if (!_store.ContainsKey(accountId))
                {
                    return Task.FromResult(0m);
                }

                var balance = _store[accountId]
                    .Sum(t => t.Type == TransactionType.Deposit ? t.Amount : -t.Amount);

                return Task.FromResult(balance);
            }
        }

        public Task<IReadOnlyList<Transaction>> GetTransactionHistory(string accountId)
        {
            lock (_lock)
            {
                if (!_store.ContainsKey(accountId))
                {
                    return Task.FromResult((IReadOnlyList<Transaction>)new List<Transaction>());
                }

                // Clone list to avoid exposing internal reference
                var history = _store[accountId].ToList();
                return Task.FromResult((IReadOnlyList<Transaction>)history);
            }
        }
    }
}