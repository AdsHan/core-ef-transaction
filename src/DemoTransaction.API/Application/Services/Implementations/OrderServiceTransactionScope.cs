using DemoTransaction.API.Application.DTO;
using DemoTransaction.API.Application.Services.Interfaces;
using DemoTransaction.Domain.Entities;
using DemoTransaction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace DemoTransaction.API.Application.Services.Implementations;

public class OrderServiceTransactionScope : IOrderServiceTransactionScope, IDisposable
{

    private readonly OrderDbContext _dbContext;

    public OrderServiceTransactionScope(OrderDbContext context)
    {
        _dbContext = context;
    }

    public async Task<int> CreateAsync(OrderInputModel inputModel)
    {
        var id = 0;

        var strategy = _dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            // Não obrigatório
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted, // Padrão                    
            };

            using var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled);

            var customer = new CustomerModel(inputModel.Customer.Name, inputModel.Customer.Phone, inputModel.Customer.Email, inputModel.Customer.CEP, inputModel.Customer.State, inputModel.Customer.City);
            _dbContext.Add(customer);
            await _dbContext.SaveChangesAsync();


            var order = new OrderModel(customer.Id, inputModel.Shipping, inputModel.Observation);
            var items = inputModel.Items.Select(i => new OrderItemModel(order.Id, i.ProductId, i.Quantity, i.UnitPrice, i.Discount, i.DiscountValue)).ToList();
            order.UpdateItems(items);
            order.CalculateTotal();
            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();

            id = order.Id;

            scope.Complete();
        });

        return id;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }

}

