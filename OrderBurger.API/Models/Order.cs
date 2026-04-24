using OrderBurger.API.Enums;

namespace OrderBurger.API.Models;

public class Order
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public string ConsumerName { get; set; } = string.Empty;
    public OrderStatus Status { get; set; } = OrderStatus.None;
    private readonly List<OrderItem> _items = new();
    public IReadOnlyList<OrderItem> Items => _items.AsReadOnly();
    public decimal SubTotal => _items.Sum(x => x.Total);
    public decimal Discount { get; set; } = decimal.Zero;
    public decimal Total => SubTotal - Discount;
    
    public Order(){}
    
    public Order(string consumerName)
    {
        Id = Guid.NewGuid();
        DateCreated = DateTime.Now;
        ConsumerName = consumerName;
    }
    
    public void AddItem(Product product, decimal quantity)
    {
        _items.Add( new OrderItem(Id, quantity, product));
    }
       
}