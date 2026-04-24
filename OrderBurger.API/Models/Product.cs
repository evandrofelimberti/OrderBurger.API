using OrderBurger.API.DTOs;
using OrderBurger.API.Enums;

namespace OrderBurger.API.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;    
    public decimal Price { get; set; }
    public CategoryEnum Category { get; set; }   
    
    public Product(){}
    
    public Product(string code, string name, string description, decimal price, CategoryEnum category)
    {
        Id = Guid.NewGuid();
        Code = code;
        Name = name;
        Description = description;
        Price = price;
        Category = category;
    }   
        
    
    public void UpdateFromRequest(ProductRequestDTO dto)
    {
        Category = dto.Category;
        Code = dto.Code;
        Description = dto.Description;
        Name = dto.Name;
        Price = dto.Price;
    }
}