using OrderBurger.API.Enums;

namespace OrderBurger.API.DTOs;

public sealed record RegisterRequestDTO(
    string UserName,
    string Email,
    string Password,
    UserRole Role
);

public sealed record LoginRequestDTO(
    string Email,
    string Password
);

public sealed record AuthResponseDTO(
    Guid UserId,
    string UserName,
    string Email,
    UserRole Role,
    string AccessToken,
    DateTime ExpiresAtUtc
);
