using MediatR;
using TinyLedger.Domain;
using System.ComponentModel.DataAnnotations;

namespace TinyLedger.Application.UseCases.Transactions;

public class RecordTransactionCommandHandler : IRequestHandler<RecordTransactionCommand>
{
    private readonly ILedgerRepository _repository;

    public RecordTransactionCommandHandler(ILedgerRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(RecordTransactionCommand request, CancellationToken cancellationToken)
    {
        if (request.Type == TransactionType.Withdraw)
        {
            var currentBalance = await _repository.GetBalance(request.AccountId);

            if (currentBalance < request.Amount)
            {
                throw new ValidationException("Insufficient balance to perform withdrawal.");
            }
        }

        var transaction = new Transaction(request.Amount, request.Type, request.Description);
        await _repository.AddTransaction(request.AccountId, transaction);
    }
}
