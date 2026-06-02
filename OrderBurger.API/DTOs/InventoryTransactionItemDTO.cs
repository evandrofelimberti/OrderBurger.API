using OrderBurger.API.Enums;

namespace OrderBurger.API.DTOs;

public sealed record InventoryTransactionItemRequestDTO(
    Guid ProductId,
    decimal Quantity
);

public sealed record InventoryTransactionItemResponseDTO(
    Guid ProductId,
    string ProductName,
    string ProductCode,
    TransactionType TransactionType,
    DateTime Date,
    decimal Quantity,
    decimal UnitPrice,
    decimal Total
);
