using DemoTransaction.Domain.Entities;
using DemoTransaction.Domain.Interfaces;

namespace DemoTransaction.Infrastructure.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly OrderDbContext _dbContext;

    public CustomerRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(CustomerModel order)
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
