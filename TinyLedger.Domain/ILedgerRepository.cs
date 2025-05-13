using System.Collections.Generic;
using System.Threading.Tasks;

namespace TinyLedger.Domain
{
    public interface ILedgerRepository
    {
        Task AddTransaction(string accountId, Transaction transaction);
        Task<decimal> GetBalance(string accountId);
        Task<IReadOnlyList<Transaction>> GetTransactionHistory(string accountId);
        
        Task CreateUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
    }
}