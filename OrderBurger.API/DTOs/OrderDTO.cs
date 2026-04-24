namespace OrderBurger.API.DTOs;

public sealed record OrderRequestDTO(
    string ConsumerName,
    IEnumerable<OrderItemRequestDTO> Items
);

public sealed record OrderResponseDTO(
    Guid Id,
    string ConsumerName,
    decimal SubTotal,
    decimal Discount,
    decimal Total,
    IEnumerable<OrderItemResponseDTO> Items
);