using Microsoft.AspNetCore.Identity;
using OrderBurger.API.DTOs;
using OrderBurger.API.Models;
using OrderBurger.API.Repositories;

namespace OrderBurger.API.Services;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        ITokenService tokenService,
        IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponseDTO> RegisterAsync(RegisterRequestDTO request, CancellationToken cancellationToken = default)
    {
        if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
            throw new InvalidOperationException("Já existe um usuário com este e-mail.");

        var user = new User(request.UserName, request.Email, string.Empty, request.Role);
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var (token, expiresAt) = _tokenService.GenerateToken(user);
        return new AuthResponseDTO(user.Id, user.UserName, user.Email, user.Role, token, expiresAt);
    }

    public async Task<AuthResponseDTO?> LoginAsync(LoginRequestDTO request, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user is null)
            return null;

        var verification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verification == PasswordVerificationResult.Failed)
            return null;

        var (token, expiresAt) = _tokenService.GenerateToken(user);
        return new AuthResponseDTO(user.Id, user.UserName, user.Email, user.Role, token, expiresAt);
    }
}
