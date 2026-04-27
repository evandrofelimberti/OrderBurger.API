using OrderBurger.API.DTOs;
using OrderBurger.API.Enums;

namespace OrderBurger.API.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OrderResponseDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<OrderResponseDTO> AddAsync(OrderRequestDTO order, CancellationToken cancellationToken = default);
    Task<OrderResponseDTO> AddItemAsync(Guid orderId, OrderItemRequestDTO item, CancellationToken cancellationToken = default);
    Task<OrderResponseDTO> RemoveItemAsync(Guid orderId, Guid productId, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<OrderResponseDTO> UpdateStatusAsync(Guid id, OrderStatus orderStatus, CancellationToken cancellationToken = default);
    
}