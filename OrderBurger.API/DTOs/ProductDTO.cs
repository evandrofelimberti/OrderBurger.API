using OrderBurger.API.Enums;

namespace OrderBurger.API.DTOs;

public record ProductRequestDTO(
    string Code, 
    string Description, 
    string Name, 
    decimal Price, 
    CategoryEnum Category);

public record ProductResponseDTO(
    Guid Id,
    string Code,
    string Description,
    string Name,
    decimal Price,
    CategoryEnum Category);