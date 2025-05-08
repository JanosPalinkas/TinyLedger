using MediatR;
using TinyLedger.Domain;

namespace TinyLedger.Application.UseCases;

public class RecordTransactionCommandHandler : IRequestHandler<RecordTransactionCommand, Unit>
{
    private readonly ILedgerRepository _repository;

    public RecordTransactionCommandHandler(ILedgerRepository repository)
    {
        _repository = repository;
    }

    public Task<Unit> Handle(RecordTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction(request.Amount, request.Type, request.Description);
        _repository.AddTransaction(request.AccountId, transaction);

        return Task.FromResult(Unit.Value);
    }
}