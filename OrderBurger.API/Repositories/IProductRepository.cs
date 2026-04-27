using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);   
    void Update(Product product);
    void Delete(Product product);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}