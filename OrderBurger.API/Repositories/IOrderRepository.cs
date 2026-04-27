using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    Task AddNewItemAsync(OrderItem item, CancellationToken cancellationToken = default);
    void RemoveItem(OrderItem item);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    void Update(Order order);
    void Delete(Order order);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    
}