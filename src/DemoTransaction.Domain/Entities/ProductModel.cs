using DemoTransaction.Domain.DomainObjects;

namespace DemoTransaction.Domain.Entities;

public class ProductModel : BaseEntity, IAggregateRoot
{

    // EF Construtor
    public ProductModel()
    {
    }

    public ProductModel(string title, string description, double price, int quantity)
    {
        Title = title;
        Description = description;
        Price = price;
        Quantity = quantity;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    // EF Rela��o        
    public List<OrderItemModel> Items { get; set; }

}
