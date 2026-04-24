using AutoMapper;
using OrderBurger.API.DTOs;
using OrderBurger.API.Models;

namespace OrderBurger.API.Mappings;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponseDTO>();
        CreateMap<ProductRequestDTO, Product>()
            .ConstructUsing(dto => new Product(dto.Code, dto.Name, dto.Description, dto.Price, dto.Category));
    }   
    
}