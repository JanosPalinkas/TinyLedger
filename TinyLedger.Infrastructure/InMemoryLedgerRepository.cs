using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyLedger.Domain;

namespace TinyLedger.Infrastructure
{
    public class InMemoryLedgerRepository : ILedgerRepository
    {
        private readonly ConcurrentDictionary<string, ConcurrentQueue<Transaction>> _store = new();

        public Task AddTransaction(string accountId, Transaction transaction)
        {
            var queue = _store.GetOrAdd(accountId, _ => new ConcurrentQueue<Transaction>());
            queue.Enqueue(transaction);
            return Task.CompletedTask;
        }

        public Task<decimal> GetBalance(string accountId)
        {
            if (!_store.TryGetValue(accountId, out var queue))
                return Task.FromResult(0m);

            decimal balance = 0;
            foreach (var t in queue)
            {
                balance += t.Type == TransactionType.Deposit ? t.Amount : -t.Amount;
            }

            return Task.FromResult(balance);
        }

        public Task<IReadOnlyList<Transaction>> GetTransactionHistory(string accountId)
        {
            if (!_store.TryGetValue(accountId, out var queue))
                return Task.FromResult((IReadOnlyList<Transaction>)new List<Transaction>());

            var history = queue.ToList();
            return Task.FromResult((IReadOnlyList<Transaction>)history);
        }
    }
}