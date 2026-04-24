using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderBurger.API.DTOs;
using OrderBurger.API.Services;

namespace OrderBurger.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public sealed class ProductController: ControllerBase
{
    private readonly IProductService _service;
    
    public ProductController(IProductService productService)
    {
        _service = productService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IAsyncEnumerable<ProductResponseDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        return Ok(await _service.GetAllAsync(cancellationToken));
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponseDTO),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok(await _service.GetByIdAsync(id, cancellationToken));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ProductResponseDTO),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> AddAsync([FromBody] ProductRequestDTO dto, CancellationToken cancellationToken)
    {
        var product = await _service.AddAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetByIdAsync), new {id = product.Id}, product);
        //return CreatedAtRoute("GetByIdAsync", new { id = product.Id }, product);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ProductResponseDTO),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ProductRequestDTO dto, CancellationToken cancellationToken)
    {
        var product = await _service.UpdateAsync(id, dto, cancellationToken);
        return Ok(product);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }   
}