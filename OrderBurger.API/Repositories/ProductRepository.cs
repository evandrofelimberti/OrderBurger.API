using Microsoft.EntityFrameworkCore;
using OrderBurger.API.Data;
using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public sealed class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var idList = ids.Distinct().ToList();
        return await DbSet
            .Where(p => idList.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    public IEnumerable<Product> GetByIds(IEnumerable<Guid> ids)
    {
        var idList = ids.Distinct().ToList();
        return DbSet
            .Where(p => idList.Contains(p.Id))
            .ToList();
    }
}