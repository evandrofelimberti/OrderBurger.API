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
    
    public OrderItem AddItem(Product product, decimal quantity)
    {
        var item = new OrderItem(Id, quantity, product);
        _items.Add(item);
        
        return item;
    }
    
    public void RemoveItem(Guid ProductId)
    {
        if (Status == OrderStatus.Closed)
            throw new Exception("Pedido finalizado, não é possivel adicionar remover itens");
        
        var item = _items.FirstOrDefault(x => x.ProductId == ProductId);
        if (item == null)
            throw new Exception("Produto não encontrado no pedido");
            
        _items.Remove(item);
    }
       
}