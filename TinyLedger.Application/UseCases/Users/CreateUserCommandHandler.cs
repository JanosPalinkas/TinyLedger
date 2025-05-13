using MediatR;
using TinyLedger.Domain;

namespace TinyLedger.Application.UseCases.Users;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly ILedgerRepository _repository;

    public CreateUserCommandHandler(ILedgerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User(Guid.NewGuid(), request.Email, request.Name, Guid.NewGuid(), passwordHash, request.Role);
        await _repository.CreateUserAsync(user);
        return user.Id;
    }
}