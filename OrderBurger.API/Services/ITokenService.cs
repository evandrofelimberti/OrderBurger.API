using OrderBurger.API.Models;

namespace OrderBurger.API.Services;

public interface ITokenService
{
    (string AccessToken, DateTime ExpiresAtUtc) GenerateToken(User user);
}
