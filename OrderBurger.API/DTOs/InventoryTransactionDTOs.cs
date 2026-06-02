
using OrderBurger.API.Enums;

namespace OrderBurger.API.DTOs;

public sealed record InventoryTransactionRequestDTO(
    TransactionType TransactionType,
    Guid BusinessPartnerId,
    string Observations,
    decimal Discount,
    IEnumerable<InventoryTransactionItemRequestDTO> Items
);

public sealed record InventoryTransactionResponseDTO(
    Guid Id,
    TransactionType TransactionType,
    DateTime DateCreated,
    Guid BusinessPartnerId,
    string? BusinessPartnerName,
    TransactionStatus Status,
    string Observations,
    decimal SubTotal,
    decimal Discount,
    decimal Total,
    IEnumerable<InventoryTransactionItemResponseDTO> Items
);
