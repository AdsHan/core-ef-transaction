using DemoTransaction.Domain.Enum;

namespace DemoTransaction.API.Application.DTO;

public class OrderInputModel
{
    public ShippingType Shipping { get; set; }
    public string? Observation { get; set; }
    public CustomerInputModel Customer { get; set; }
    public List<OrderItemInputModel> Items { get; set; }
}