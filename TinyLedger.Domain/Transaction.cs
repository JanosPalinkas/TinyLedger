namespace TinyLedger.Domain;

public class Transaction
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public decimal Amount { get; init; }
    public TransactionType Type { get; init; }
    public string Description { get; init; } = string.Empty;

    public Transaction(decimal amount, TransactionType type, string description = "")
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        Amount = amount;
        Type = type;
        Description = description;
    }

    public decimal GetSignedAmount() => Type == TransactionType.Deposit ? Amount : -Amount;
}
