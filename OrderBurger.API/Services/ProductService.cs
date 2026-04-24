using AutoMapper;
using OrderBurger.API.DTOs;
using OrderBurger.API.Exceptions;
using OrderBurger.API.Models;
using OrderBurger.API.Repositories;

namespace OrderBurger.API.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private ILogger<ProductService> _logger;
    
    public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<IEnumerable<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken)
    {
        var items = await _productRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProductResponseDTO>>(items);
    }

    public async Task<ProductResponseDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<ProductResponseDTO>(product);
    }

    public async Task<ProductResponseDTO> AddAsync(ProductRequestDTO product, CancellationToken cancellationToken)
    {
        var productEntity = _mapper.Map<Product>(product);
        
        await _productRepository.AddAsync(productEntity, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Produto criado com id: {id}", productEntity.Id);       
        return _mapper.Map<ProductResponseDTO>(productEntity);       
    }

    public async Task<ProductResponseDTO> UpdateAsync(Guid id, ProductRequestDTO dto, CancellationToken cancellationToken)
    {
        var product = await GetByIdThrowAsync(id, cancellationToken);
        
        product.UpdateFromRequest(dto);
        
        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync(cancellationToken);  
        
        _logger.LogInformation("Produto atualizado com id: {id}", product.Id);
        
        return _mapper.Map<ProductResponseDTO>(product);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await GetByIdThrowAsync(id, cancellationToken);
        
        _productRepository.Delete(product);
        await _productRepository.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Produto deletado com id: {id}", product.Id);       
    }

    private async Task<Product> GetByIdThrowAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _productRepository.GetByIdAsync(id, cancellationToken)
               ?? throw new ProductNotFoundException(id);

    }
}