using OrderBurger.API.Enums;

namespace OrderBurger.API.Models;

public class InventoryTransaction
{
    public Guid Id { get; set; }
    public TransactionType TransactionType { get; set; } = TransactionType.None;
    public DateTime DateCreated { get; set; }
    public Guid BusinessPartnerId { get; set; }
    public BusinessPartner? BusinessPartner { get; private set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.None;
    public string Observations { get; set; } = string.Empty;

    private readonly List<InventoryTransactionItem> _items = new();
    public IReadOnlyList<InventoryTransactionItem> Items => _items.AsReadOnly();

    public decimal SubTotal => _items.Sum(x => x.Total);
    public decimal Discount { get; private set; } = decimal.Zero;
    public decimal Total => SubTotal - Discount;

    public InventoryTransaction() { }

    public InventoryTransaction(TransactionType transactionType, BusinessPartner businessPartner, string observations)
    {
        Id = Guid.NewGuid();
        DateCreated = DateTime.UtcNow;
        TransactionType = transactionType;
        BusinessPartnerId = businessPartner.Id;
        BusinessPartner = businessPartner;
        Observations = observations;
        Status = TransactionStatus.Open;
    }

    public InventoryTransactionItem AddItem(Product product, decimal quantity)
    {
        var item = new InventoryTransactionItem(Id, TransactionType, quantity, product);
        _items.Add(item);

        return item;
    }

    public void RemoveItem(Guid productId)
    {
        var item = _items.FirstOrDefault(x => x.ProductId == productId);
        if (item == null)
            throw new Exception("Produto não encontrado na movimentação");

        _items.Remove(item);
    }

    public void ApplyDiscount(decimal discount)
    {
        Discount = discount;
    }
}
