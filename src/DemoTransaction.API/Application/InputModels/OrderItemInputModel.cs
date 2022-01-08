using DemoTransaction.Domain.Enum;

namespace DemoTransaction.API.Application.DTO;

public class OrderItemInputModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DiscountType Discount { get; set; }
    public decimal DiscountValue { get; set; }
}

