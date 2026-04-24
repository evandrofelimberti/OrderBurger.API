using OrderBurger.API.DTOs;

namespace OrderBurger.API.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductResponseDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ProductResponseDTO> AddAsync(ProductRequestDTO product, CancellationToken cancellationToken = default);

    Task<ProductResponseDTO> UpdateAsync(Guid id, ProductRequestDTO product,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}