using Microsoft.EntityFrameworkCore;
using OrderBurger.API.Data;
using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public sealed class BusinessPartnerRepository : RepositoryBase<BusinessPartner>, IBusinessPartnerRepository
{
    public BusinessPartnerRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<BusinessPartner?> GetByDocumentNumberAsync(string documentNumber, CancellationToken cancellationToken = default)
    {
        return await Context.BusinessPartners
            .FirstOrDefaultAsync(x => x.DocumentNumber == documentNumber, cancellationToken);
    }
}
