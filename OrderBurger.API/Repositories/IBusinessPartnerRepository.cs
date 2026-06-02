using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public interface IBusinessPartnerRepository : IRepositoryBase<BusinessPartner>
{
    Task<BusinessPartner?> GetByDocumentNumberAsync(string documentNumber, CancellationToken cancellationToken = default);
}
