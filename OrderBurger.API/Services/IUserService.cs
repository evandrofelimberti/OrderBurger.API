using OrderBurger.API.DTOs;

namespace OrderBurger.API.Services;

public interface IUserService
{
    Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request, CancellationToken cancellationToken = default);
    Task<AuthResponseDTO?> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default);
}
