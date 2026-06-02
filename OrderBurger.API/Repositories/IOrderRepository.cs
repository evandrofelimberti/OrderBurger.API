using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public interface IOrderRepository : IRepositoryBase<Order>
{
    Task AddNewItemAsync(OrderItem item, CancellationToken cancellationToken = default);
    void RemoveItem(OrderItem item);
}