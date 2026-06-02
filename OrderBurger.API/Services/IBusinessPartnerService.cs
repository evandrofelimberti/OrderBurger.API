using OrderBurger.API.DTOs;

namespace OrderBurger.API.Services;

public interface IBusinessPartnerService
{
    Task<BusinessPartnerResponseDTO> CreateAsync(BusinessPartnerRequestDTO request, CancellationToken cancellationToken = default);
    Task<BusinessPartnerResponseDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<BusinessPartnerResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<BusinessPartnerResponseDTO?> UpdateAsync(Guid id, BusinessPartnerRequestDTO request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
