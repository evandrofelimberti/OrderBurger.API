using OrderBurger.API.Enums;

namespace OrderBurger.API.Models;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public CategoryEnum Category { get; set; }
    public decimal Quantity { get; set; } 
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;
    
    public OrderItem(){}
    public OrderItem(Guid orderId, decimal quantity, Product product)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        ProductId = product.Id;
        Category = product.Category;
        Quantity = quantity;
        UnitPrice = product.Price;
    }   
}