using DemoTransaction.Domain.Interfaces;
using DemoTransaction.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DemoTransaction.Infrastructure.Data.UoW;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly OrderDbContext _dbContext;

    public UnitOfWork(OrderDbContext context)
    {
        _dbContext = context;
    }

    private IOrderRepository _orderRepository;
    public IOrderRepository Orders
    {
        get => _orderRepository ?? (_orderRepository = new OrderRepository(_dbContext));
    }

    private ICustomerRepository _customerRepository;
    public ICustomerRepository Customers
    {
        get => _customerRepository ?? (_customerRepository = new CustomerRepository(_dbContext));
    }

    public int SaveChanges()
    {
        return _dbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _dbContext.Database.BeginTransaction();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _dbContext.Database.BeginTransactionAsync();
    }

    public IExecutionStrategy CreateStrategy()
    {
        return _dbContext.Database.CreateExecutionStrategy();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}

