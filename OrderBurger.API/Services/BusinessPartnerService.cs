using AutoMapper;
using OrderBurger.API.DTOs;
using OrderBurger.API.Models;
using OrderBurger.API.Repositories;

namespace OrderBurger.API.Services;

public sealed class BusinessPartnerService : IBusinessPartnerService
{
    private readonly IBusinessPartnerRepository _repository;
    private readonly IMapper _mapper;

    public BusinessPartnerService(IBusinessPartnerRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BusinessPartnerResponseDTO> CreateAsync(BusinessPartnerRequestDTO request, CancellationToken cancellationToken = default)
    {
        var partner = new BusinessPartner(request.Name, request.DocumentNumber, request.DocumentType, request.Type);

        await _repository.AddAsync(partner, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BusinessPartnerResponseDTO>(partner);
    }

    public async Task<BusinessPartnerResponseDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var partner = await _repository.GetByIdAsync(id, cancellationToken);
        return partner is null ? null : _mapper.Map<BusinessPartnerResponseDTO>(partner);
    }

    public async Task<IEnumerable<BusinessPartnerResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var partners = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<BusinessPartnerResponseDTO>>(partners);
    }

    public async Task<BusinessPartnerResponseDTO?> UpdateAsync(Guid id, BusinessPartnerRequestDTO request, CancellationToken cancellationToken = default)
    {
        var partner = await _repository.GetByIdAsync(id, cancellationToken);
        if (partner is null)
            return null;

        partner.UpdateFromRequest(request);

        _repository.Update(partner);
        await _repository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<BusinessPartnerResponseDTO>(partner);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var partner = await _repository.GetByIdAsync(id, cancellationToken);
        if (partner is null)
            return false;

        _repository.Delete(partner);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }
}
