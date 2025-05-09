using MediatR;
using TinyLedger.Domain;

namespace TinyLedger.Application.UseCases.Transactions;

public class GetTransactionHistoryQuery : IRequest<IReadOnlyList<Transaction>>
{
    public string AccountId { get; }

    public GetTransactionHistoryQuery(string accountId)
    {
        AccountId = accountId;
    }
}
