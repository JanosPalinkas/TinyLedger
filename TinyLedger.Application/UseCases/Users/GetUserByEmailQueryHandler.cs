using MediatR;
using TinyLedger.Domain;

namespace TinyLedger.Application.UseCases.Users;

public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User?>
{
    private readonly ILedgerRepository _repository;

    public GetUserByEmailQueryHandler(ILedgerRepository repository)
    {
        _repository = repository;
    }

    public async Task<User?> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetUserByEmailAsync(request.Email);
    }
}