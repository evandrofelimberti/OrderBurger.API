using OrderBurger.API.Enums;

namespace OrderBurger.API.DTOs;

public sealed record BusinessPartnerRequestDTO(
    string Name,
    string DocumentNumber,
    DocumentType DocumentType,
    BusinessPartnerType Type
);

public sealed record BusinessPartnerResponseDTO(
    Guid Id,
    string Name,
    string DocumentNumber,
    DocumentType DocumentType,
    BusinessPartnerType Type
);
