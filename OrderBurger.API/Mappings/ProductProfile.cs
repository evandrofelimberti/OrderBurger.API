using AutoMapper;
using OrderBurger.API.DTOs;
using OrderBurger.API.Models;

namespace OrderBurger.API.Mappings;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<ProductRequestDTO, Product>();        
        CreateMap<Product, ProductResponseDTO>();
    }   
    
}