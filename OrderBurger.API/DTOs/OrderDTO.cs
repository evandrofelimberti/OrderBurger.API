using OrderBurger.API.Enums;
using OrderBurger.API.Models;

namespace OrderBurger.API.DTOs;

public sealed record OrderRequestDTO(
    string CustomerName,
    IEnumerable<OrderItemRequestDTO> Items
);

public sealed record OrderResponseDTO(
    Guid Id,
    string CustomerName,
    decimal SubTotal,
    decimal Discount,
    decimal Total,
    OrderStatus Status,
    IEnumerable<OrderItemResponseDTO> Items
);