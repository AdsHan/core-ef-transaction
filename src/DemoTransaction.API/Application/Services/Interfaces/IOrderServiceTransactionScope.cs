using DemoTransaction.API.Application.DTO;

namespace DemoTransaction.API.Application.Services.Interfaces;

public interface IOrderServiceTransactionScope
{
    Task<int> CreateAsync(OrderInputModel inputModel);
}

