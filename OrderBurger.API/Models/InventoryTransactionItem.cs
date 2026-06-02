using OrderBurger.API.Enums;

namespace OrderBurger.API.Models;

public class InventoryTransactionItem
{
    public Guid Id { get; set; }
    public Guid InventoryTransactionId { get; set; }
    public Guid ProductId { get; set; }
    public TransactionType TransactionType { get; set; } = TransactionType.None;
    public DateTime Date { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Total => Quantity * UnitPrice;

    public Product? Product { get; private set; }

    public InventoryTransactionItem() { }

    public InventoryTransactionItem(Guid inventoryTransactionId, TransactionType transactionType, decimal quantity, Product product)
    {
        Id = Guid.NewGuid();
        InventoryTransactionId = inventoryTransactionId;
        TransactionType = transactionType;
        Date = DateTime.UtcNow;
        ProductId = product.Id;
        Product = product;
        Quantity = quantity;
        UnitPrice = product.Price;
    }
}
