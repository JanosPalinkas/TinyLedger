using MediatR;
using TinyLedger.Domain;

namespace TinyLedger.Application.UseCases.Transactions;

public class GetTransactionHistoryQueryHandler : IRequestHandler<GetTransactionHistoryQuery, IReadOnlyList<Transaction>>
{
    private readonly ILedgerRepository _ledgerRepository;

    public GetTransactionHistoryQueryHandler(ILedgerRepository ledgerRepository)
    {
        _ledgerRepository = ledgerRepository;
    }

    public Task<IReadOnlyList<Transaction>> Handle(GetTransactionHistoryQuery request, CancellationToken cancellationToken)
    {
        return _ledgerRepository.GetTransactionHistory(request.AccountId);
    }
}