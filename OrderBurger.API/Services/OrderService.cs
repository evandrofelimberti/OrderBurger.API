using AutoMapper;
using OrderBurger.API.Business.OrderDiscount;
using OrderBurger.API.DTOs;
using OrderBurger.API.Enums;
using OrderBurger.API.Exceptions;
using OrderBurger.API.Models;
using OrderBurger.API.Repositories;

namespace OrderBurger.API.Services;

public sealed class OrderService: IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;
    private readonly IOrderDiscountStrategy _orderDiscountStrategy;
    
    public OrderService(IOrderRepository orderRepository, 
        IProductRepository productRepository, 
        IMapper mapper, 
        ILogger<OrderService> logger, 
        IOrderDiscountStrategy orderDiscountStrategy)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _logger = logger;
        _orderDiscountStrategy = orderDiscountStrategy;
    }
    
    public async Task<IEnumerable<OrderResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
       var orders = await _orderRepository.GetAllAsync(cancellationToken);
       return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
    }

    public async Task<OrderResponseDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await _orderRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<OrderResponseDTO>(order);
        
    }

    public async Task<OrderResponseDTO> AddAsync(OrderRequestDTO orderDto, CancellationToken cancellationToken = default)
    {
       var productIds = orderDto.Items.Select(i => i.ProductId).ToList();
       
       var products = (await _productRepository.GetByIdsAsync(productIds, cancellationToken)).ToDictionary(p => p.Id);
       
       var productNotFound = productIds.Except(products.Keys).ToList();
       if (productNotFound.Count != 0)
           throw new ProductNotFoundException(productNotFound.First());

       var order = new Order(orderDto.ConsumerName);
       foreach (var item in orderDto.Items)
       {
           var product = products[item.ProductId];
           var newitem = order.AddItem(product, item.Quantity);
       }
       
       ApplyDiscount(order);
       await _orderRepository.AddAsync(order, cancellationToken);
       await _orderRepository.SaveChangesAsync(cancellationToken);
       
       _logger.LogInformation(
           "Pedido criado: {OrderId} | Cliente: {Customer} | Itens: {Count} | Total: {Total:C}",
           order.Id, order.ConsumerName, order.Items.Count, order.Total);
       
       return _mapper.Map<OrderResponseDTO>(order);
    }

    public async Task<OrderResponseDTO> AddItemAsync(Guid orderId, OrderItemRequestDTO item, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(orderId, cancellationToken);
        
         if (HasProductDuplicated(order, item.ProductId)) 
             throw new OrderProductDuplicate();        
        
        var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
        if (product == null)
            throw new ProductNotFoundException(item.ProductId);
        
        var newItem = order.AddItem(product, item.Quantity);
        
        await _orderRepository.AddNewItemAsync(newItem, cancellationToken);
        
        ApplyDiscount(order);
        await _orderRepository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Item adicionado ao pedido: {OrderId} | Produto: {ProductName} | Quantidade: {Quantity}",
            order.Id, product?.Name, item.Quantity);
        
        return _mapper.Map<OrderResponseDTO>(order);
    }

    public async Task<OrderResponseDTO> RemoveItemAsync(Guid orderId, Guid productId, CancellationToken cancellationToken = default)
    {
       var order = await GetOrderOrThrowAsync(orderId, cancellationToken);
       
       var itemRemove = order.Items.FirstOrDefault(p => p.ProductId == productId) ?? throw new ProductNotFoundException(productId);
       
       order.RemoveItem(productId);
       
       _orderRepository.RemoveItem(itemRemove);
       
       ApplyDiscount(order);    
       _orderRepository.Update(order);
       await _orderRepository.SaveChangesAsync(cancellationToken);
       
       _logger.LogInformation("Item removido do pedido: {OrderId} | Produto: {ProductName}", order.Id, productId);
       
       return _mapper.Map<OrderResponseDTO>(order);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(id, cancellationToken);
        
        _orderRepository.Delete(order);
        await _orderRepository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Pedido deletado: {OrderId}", order.Id);      

    }

    public async Task<OrderResponseDTO> UpdateStatusAsync(Guid id, OrderStatus orderStatus, CancellationToken cancellationToken = default)
    {
        var order = await GetOrderOrThrowAsync(id, cancellationToken);
        
        order.Status = orderStatus;
        
        _orderRepository.Update(order);
        await _orderRepository.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Status do pedido atualizado: {OrderId} | Status: {Status}", order.Id, order.Status);      
        
        return _mapper.Map<OrderResponseDTO>(order);      
    }
    
    private async Task<Order> GetOrderOrThrowAsync(Guid orderId, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
        if (order is null)
            throw new KeyNotFoundException("Pedido não encontrado");

        return order;
    }

    private void ApplyDiscount(Order order)
    {
        var calculateDiscount = _orderDiscountStrategy.CalculateDiscount(order);
        order.ApplyDiscount(calculateDiscount);
    }
    
    private bool HasProductDuplicated(Order order, Guid productId)
    {
        return order.Items.Any(p => p.ProductId == productId);
        
    }
}