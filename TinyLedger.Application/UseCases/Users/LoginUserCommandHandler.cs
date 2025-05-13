using MediatR;
using TinyLedger.Domain;
using BCrypt.Net;
using TinyLedger.Application.Services;

namespace TinyLedger.Application.UseCases.Users;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
{
    private readonly ILedgerRepository _repository;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(ILedgerRepository repository, ITokenService tokenService)
    {
        _repository = repository;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserByEmailAsync(request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        return _tokenService.GenerateToken(user);
    }
}