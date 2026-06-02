using Microsoft.EntityFrameworkCore;
using OrderBurger.API.Data;
using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public sealed class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Users
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await Context.Users
            .AnyAsync(x => x.Email == email, cancellationToken);
    }
}
