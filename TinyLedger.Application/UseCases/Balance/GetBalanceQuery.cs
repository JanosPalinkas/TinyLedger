using MediatR;

namespace TinyLedger.Application.UseCases.Balance;

public class GetBalanceQuery : IRequest<decimal>
{
    public string AccountId { get; }

    public GetBalanceQuery(string accountId)
    {
        AccountId = accountId;
    }
}
