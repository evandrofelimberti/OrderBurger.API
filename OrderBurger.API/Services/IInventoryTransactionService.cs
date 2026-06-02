using OrderBurger.API.DTOs;

namespace OrderBurger.API.Services;

public interface IInventoryTransactionService
{
    Task<InventoryTransactionResponseDTO> CreateAsync(InventoryTransactionRequestDTO request, CancellationToken cancellationToken = default);
    Task<InventoryTransactionResponseDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<InventoryTransactionResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
}
