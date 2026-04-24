using OrderBurger.API.Enums;

namespace OrderBurger.API.DTOs;

public sealed record OrderItemRequestDTO(
    Guid ProductId, 
    decimal Quantity
);

public sealed record OrderItemResponseDTO(
    Guid ProductId, 
    string ProductName,
    string ProductCode,
    decimal Quantity,
    decimal UnitPrice,
    decimal Total
);