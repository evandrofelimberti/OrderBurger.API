using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
}
