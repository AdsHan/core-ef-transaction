using DemoTransaction.API.Application.DTO;
using DemoTransaction.API.Application.Services.Interfaces;
using DemoTransaction.Domain.Entities;
using DemoTransaction.Infrastructure.Data.UoW;
using Microsoft.EntityFrameworkCore;

namespace DemoTransaction.API.Application.Services.Implementations;

public class OrderServiceUoW : IOrderServiceUoW
{

    private readonly IUnitOfWork _unitOfWork;

    public OrderServiceUoW(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> CreateAsync(OrderInputModel inputModel)
    {
        var id = 0;

        var strategy = _unitOfWork.CreateStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var customer = new CustomerModel(inputModel.Customer.Name, inputModel.Customer.Phone, inputModel.Customer.Email, inputModel.Customer.CEP, inputModel.Customer.State, inputModel.Customer.City);
                _unitOfWork.Customers.Add(customer);
                await _unitOfWork.SaveChangesAsync();


                var order = new OrderModel(customer.Id, inputModel.Shipping, inputModel.Observation);
                var items = inputModel.Items.Select(i => new OrderItemModel(order.Id, i.ProductId, i.Quantity, i.UnitPrice, i.Discount, i.DiscountValue)).ToList();
                order.UpdateItems(items);
                order.CalculateTotal();
                _unitOfWork.Orders.Add(order);
                await _unitOfWork.SaveChangesAsync();


                await transaction.CommitAsync();


                id = order.Id;
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        });

        return id;
    }
}

