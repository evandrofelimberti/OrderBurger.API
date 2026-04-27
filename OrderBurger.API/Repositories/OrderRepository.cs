using Microsoft.EntityFrameworkCore;
using OrderBurger.API.Data;
using OrderBurger.API.Models;

namespace OrderBurger.API.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
    }

    public async Task AddNewItemAsync(OrderItem item, CancellationToken cancellationToken = default)
    {
        await _context.Set<OrderItem>().AddAsync(item, cancellationToken);        
    }

    public void RemoveItem(OrderItem item)
    {
        _context.Set<OrderItem>().Remove(item);     
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Product)
            .AsQueryable();
        
        return await query.ToListAsync(cancellationToken);
    }

    public void Update(Order order)
    {
        //_context.Orders.Update(order);
        _context.Entry(order).State = EntityState.Modified;

        foreach (var item in order.Items)
        {
            var entry = _context.Entry(item);

            if (entry.State == EntityState.Detached)
                _context.Set<OrderItem>().Add(item);
        }
        
    }

    public void Delete(Order order)
    {
        _context.Orders.Remove(order);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.AnyAsync(o => o.Id == id, cancellationToken);
    }
}