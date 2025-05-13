using TinyLedger.Domain;

namespace TinyLedger.Application.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}