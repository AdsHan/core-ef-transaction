using DemoTransaction.API.Application.DTO;

namespace DemoTransaction.API.Application.Services.Interfaces;

public interface IOrderServiceUoW
{
    Task<int> CreateAsync(OrderInputModel inputModel);
}

