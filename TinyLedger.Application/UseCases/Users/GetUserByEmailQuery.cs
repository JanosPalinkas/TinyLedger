using MediatR;
using TinyLedger.Domain;

namespace TinyLedger.Application.UseCases.Users;

public class GetUserByEmailQuery : IRequest<User?>
{
    public string Email { get; }

    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }
}