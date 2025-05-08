using MediatR;
using TinyLedger.Domain;

namespace TinyLedger.Application.UseCases;

public class RecordTransactionCommand : IRequest
{
    public string AccountId { get; set; } = string.Empty;
    public decimal Amount { get; }
    public TransactionType Type { get; }
    public string Description { get; }

    public RecordTransactionCommand(decimal amount, TransactionType type, string description)
    {
        Amount = amount;
        Type = type;
        Description = description;
    }
}