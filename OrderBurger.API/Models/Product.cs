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
}