using DemoTransaction.API.Application.DTO;
using DemoTransaction.API.Application.Services.Interfaces;
using DemoTransaction.Domain.Entities;
using DemoTransaction.Domain.Interfaces;

namespace DemoTransaction.API.Application.Services.Implementations;

public class OrderService : IOrderService
{

    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;

    public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
    }

    public async Task<int> CreateAsync(OrderInputModel inputModel)
    {
        try
        {
            var customer = new CustomerModel(inputModel.Customer.Name, inputModel.Customer.Phone, inputModel.Customer.Email, inputModel.Customer.CEP, inputModel.Customer.State, inputModel.Customer.City);
            _customerRepository.Add(customer);
            await _customerRepository.SaveAsync();


            var order = new OrderModel(customer.Id, inputModel.Shipping, inputModel.Observation);
            var items = inputModel.Items.Select(i => new OrderItemModel(order.Id, i.ProductId, i.Quantity, i.UnitPrice, i.Discount, i.DiscountValue)).ToList();
            order.UpdateItems(items);
            order.CalculateTotal();
            _orderRepository.Add(order);
            await _orderRepository.SaveAsync();

            return order.Id;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao salvar o pedido");
        }

    }
}

