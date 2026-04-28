using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrderBurger.API.DTOs;
using OrderBurger.API.Services;

namespace OrderBurger.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public sealed class OrdersController: ControllerBase
{
    private readonly IOrderService _service;
     
    public OrdersController(IOrderService orderService)
    {
        _service = orderService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IAsyncEnumerable<OrderResponseDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        return Ok(await _service.GetAllAsync(cancellationToken));
    }
    
    [HttpGet("{id:guid}", Name = "GetOrderById")]
    [ProducesResponseType(typeof(OrderResponseDTO),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _service.GetByIdAsync(id, cancellationToken));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(OrderResponseDTO),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OrderResponseDTO),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddAsync([FromBody] OrderRequestDTO dto, CancellationToken cancellationToken)
    {
        var order = await _service.AddAsync(dto, cancellationToken);
        return CreatedAtRoute("GetOrderById", new { id = order.Id }, order);
    }
    
    [HttpPost("{orderId:guid}/items")]
    [ProducesResponseType(typeof(OrderResponseDTO),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OrderResponseDTO),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddItemAsync(Guid orderId, [FromBody] OrderItemRequestDTO dto, CancellationToken cancellationToken)
    {
        var order = await _service.AddItemAsync(orderId, dto, cancellationToken);
        return Ok(order);
    }
    
    [HttpDelete("{orderId:guid}/items/{productId:guid}")]
    [ProducesResponseType(typeof(OrderResponseDTO),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> RemoveItemAsync(Guid orderId, Guid productId, CancellationToken cancellationToken)
    {
        return Ok(await _service.RemoveItemAsync(orderId, productId, cancellationToken));
    }
    
}