using Microsoft.EntityFrameworkCore;
using OrderBurger.API.Data;
using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public sealed class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public override async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .ToListAsync(cancellationToken);
    }

    public override void Update(Order order)
    {
        Context.Entry(order).State = EntityState.Modified;
    }

    public override async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(o => o.Id == id, cancellationToken);
    }

    public async Task AddNewItemAsync(OrderItem item, CancellationToken cancellationToken = default)
    {
        await Context.Set<OrderItem>().AddAsync(item, cancellationToken);
    }

    public void RemoveItem(OrderItem item)
    {
        Context.Set<OrderItem>().Remove(item);
    }
}