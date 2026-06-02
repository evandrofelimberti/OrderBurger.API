using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    IEnumerable<Product> GetByIds(IEnumerable<Guid> ids);
}