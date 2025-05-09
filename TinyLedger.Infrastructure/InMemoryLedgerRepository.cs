using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyLedger.Domain;

namespace TinyLedger.Infrastructure
{
    public class InMemoryLedgerRepository : ILedgerRepository
    {
        private readonly ConcurrentDictionary<string, List<Transaction>> _store = new();

        public Task AddTransaction(string accountId, Transaction transaction)
        {
            var list = _store.GetOrAdd(accountId, _ => new List<Transaction>());

            lock (list)
            {
                list.Add(transaction);
            }

            return Task.CompletedTask;
        }

        public Task<decimal> GetBalance(string accountId)
        {
            if (!_store.TryGetValue(accountId, out var list))
                return Task.FromResult(0m);

            lock (list)
            {
                var balance = list.Sum(t =>
                    t.Type == TransactionType.Deposit ? t.Amount : -t.Amount);
                return Task.FromResult(balance);
            }
        }

        public Task<IReadOnlyList<Transaction>> GetTransactionHistory(string accountId)
        {
            if (!_store.TryGetValue(accountId, out var list))
                return Task.FromResult((IReadOnlyList<Transaction>)new List<Transaction>());

            lock (list)
            {
                var copy = list.ToList();
                return Task.FromResult((IReadOnlyList<Transaction>)copy);
            }
        }
    }
}