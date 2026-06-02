using OrderBurger.API.Data;
using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public sealed class InventoryTransactionRepository : RepositoryBase<InventoryTransaction>, IInventoryTransactionRepository
{
    public InventoryTransactionRepository(AppDbContext context) : base(context)
    {
    }
}
