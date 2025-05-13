using MediatR;

namespace TinyLedger.Application.UseCases.Users;

public class LoginUserCommand : IRequest<string>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}