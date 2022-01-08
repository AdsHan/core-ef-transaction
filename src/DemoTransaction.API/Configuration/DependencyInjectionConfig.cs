using DemoTransaction.API.Application.Services.Implementations;
using DemoTransaction.API.Application.Services.Interfaces;
using DemoTransaction.API.Infrastructure;
using DemoTransaction.API.Interceptor;
using DemoTransaction.Domain.Interfaces;
using DemoTransaction.Infrastructure.Data;
using DemoTransaction.Infrastructure.Data.Repositories;
using DemoTransaction.Infrastructure.Data.UoW;
using Microsoft.EntityFrameworkCore;

namespace BackgroundJobs.API.Configuration;

public static class DependencyInjectionConfig
{
    //private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<OrderDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("SQLServerCs"),
                    p => p.EnableRetryOnFailure(
                            maxRetryCount: 1,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null).
                            MigrationsHistoryTable("EFMigrations"))
                .LogTo(Console.WriteLine, LogLevel.Information)
                //.UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .AddInterceptors(new InterceptorTransaction()));


        services.AddTransient<ProductPopulateService>();

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IOrderServiceTransactionScope, OrderServiceTransactionScope>();
        services.AddScoped<IOrderServiceUoW, OrderServiceUoW>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;

    }
}
