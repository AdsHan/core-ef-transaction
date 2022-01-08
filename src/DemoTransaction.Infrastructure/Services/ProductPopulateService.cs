using DemoTransaction.Domain.Entities;
using DemoTransaction.Domain.Enum;
using DemoTransaction.Infrastructure.Data;

namespace DemoTransaction.API.Infrastructure;

public class ProductPopulateService
{
    private readonly OrderDbContext _dbContext;

    public ProductPopulateService(OrderDbContext context)
    {
        _dbContext = context;
    }

    public async Task InitializeAsync()
    {
        if (_dbContext.Database.EnsureCreated())
        {
            _dbContext.Products.Add(new ProductModel()
            {
                Title = "Sandalia",
                Description = "Sandalia Preta Couro Salto Fino",
                Price = 249.50,
                Quantity = 100
            });

            _dbContext.Products.Add(new ProductModel()
            {
                Title = "Sapatilha",
                Description = "Sapatilha Tecido Platino",
                Price = 142.50,
                Quantity = 25,
                Status = EntityStatusEnum.Inactive
            });

            _dbContext.Products.Add(new ProductModel()
            {
                Title = "Chinelo",
                Description = "Chinelo Tradicional AdultoUnissex",
                Price = 60.50,
                Quantity = 50
            });

            _dbContext.Products.Add(new ProductModel()
            {
                Title = "Tênis",
                Description = "Chinelo Tradicional AdultoUnissex",
                Price = 60.50,
                Quantity = 50,
                Status = EntityStatusEnum.Inactive
            });

            _dbContext.Customers.Add(new CustomerModel()
            {
                Name = "Cliente Demonstração",
                Phone = "51999123456",
                Email = "teste@gamil.com",
                CEP = "99900000",
                City = "Porto Alegre",
                State = "RS"
            });

            _dbContext.SaveChanges();
        };
    }

}
