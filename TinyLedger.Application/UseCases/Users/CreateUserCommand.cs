using MediatR;

namespace TinyLedger.Application.UseCases.Users;
 
public class CreateUserCommand : IRequest<Guid>
{
    public string Email { get; }
    public string Name { get; }
    public string Password { get; }
    public string Role { get; }

    public CreateUserCommand(string email, string name, string password, string role)
    {
        Email = email;
        Name = name;
        Password = password;
        Role = role;
    }
}