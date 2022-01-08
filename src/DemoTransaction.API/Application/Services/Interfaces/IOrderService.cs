using DemoTransaction.API.Application.DTO;

namespace DemoTransaction.API.Application.Services.Interfaces;

public interface IOrderService
{
    Task<int> CreateAsync(OrderInputModel inputModel);
}

