using MediatR;
using TinyLedger.Domain;

namespace TinyLedger.Application.UseCases.Balance;

public class GetBalanceQueryHandler : IRequestHandler<GetBalanceQuery, decimal>
{
    private readonly ILedgerRepository _ledgerRepository;

    public GetBalanceQueryHandler(ILedgerRepository ledgerRepository)
    {
        _ledgerRepository = ledgerRepository;
    }

    public Task<decimal> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        return _ledgerRepository.GetBalance(request.AccountId);
    }
}