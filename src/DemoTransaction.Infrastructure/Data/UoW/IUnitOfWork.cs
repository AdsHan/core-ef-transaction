using DemoTransaction.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace DemoTransaction.Infrastructure.Data.UoW;

public interface IUnitOfWork
{
    int SaveChanges();
    Task<int> SaveChangesAsync();
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync();
    IExecutionStrategy CreateStrategy();

    public IOrderRepository Orders { get; }
    public ICustomerRepository Customers { get; }
}

