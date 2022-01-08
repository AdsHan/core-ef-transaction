using DemoTransaction.Domain.Entities;
using DemoTransaction.Domain.Interfaces;

namespace DemoTransaction.Infrastructure.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(OrderModel order)
    {
        _dbContext.Add(order);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
