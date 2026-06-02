using AutoMapper;
using OrderBurger.API.DTOs;
using OrderBurger.API.Exceptions;
using OrderBurger.API.Models;
using OrderBurger.API.Repositories;

namespace OrderBurger.API.Services;

public sealed class InventoryTransactionService : IInventoryTransactionService
{
    private readonly IInventoryTransactionRepository _repository;
    private readonly IBusinessPartnerRepository _businessPartnerRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public InventoryTransactionService(
        IInventoryTransactionRepository repository,
        IBusinessPartnerRepository businessPartnerRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _repository = repository;
        _businessPartnerRepository = businessPartnerRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }
    

    public async Task<InventoryTransactionResponseDTO> CreateAsync(InventoryTransactionRequestDTO request, CancellationToken cancellationToken = default)
    {
        var partner = await _businessPartnerRepository.GetByIdAsync(request.BusinessPartnerId, cancellationToken);
        if (partner is null)
            throw new Exception("Parceiro de negócio não encontrado.");

        var transaction = new InventoryTransaction(request.TransactionType, partner, request.Observations);

        foreach (var itemDto in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemDto.ProductId, cancellationToken)
                ?? throw new ProductNotFoundException(itemDto.ProductId);

            transaction.AddItem(product, itemDto.Quantity);
        }

        transaction.ApplyDiscount(request.Discount);

        await _repository.AddAsync(transaction, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<InventoryTransactionResponseDTO>(transaction);
    }

    public async Task<InventoryTransactionResponseDTO?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var transaction = await _repository.GetByIdAsync(id, cancellationToken);
        return transaction is null ? null : _mapper.Map<InventoryTransactionResponseDTO>(transaction);
    }

    public async Task<IEnumerable<InventoryTransactionResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var transactions = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<InventoryTransactionResponseDTO>>(transactions);
    }
}
