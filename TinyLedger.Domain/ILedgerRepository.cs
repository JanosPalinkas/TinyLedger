namespace TinyLedger.Domain;

public interface ILedgerRepository
{
    void AddTransaction(Transaction transaction); // For single-account mode
    void AddTransaction(string accountId, Transaction transaction); // Optional
    IReadOnlyList<Transaction> GetTransactions(); // For single-account mode
    Task<IReadOnlyList<Transaction>> GetTransactionHistory(string accountId);
    Task<decimal> GetBalance(string accountId);
}