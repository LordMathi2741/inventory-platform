using Management.Domain.Model.Commands;
using MongoDB.Bson;

namespace Management.Domain.Model.Aggregates;

public partial class Product
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}

public partial class Product
{
    public Product()
    {
        Name = string.Empty;
        Description = string.Empty;
        Price = 0;
        Stock = 0;
    }

    public Product(CreateProductCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        Price = command.Price;
        Stock = command.Stock;
    }
    
    public void Update(UpdateProductCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        Price = command.Price;
        Stock = command.Stock;
    }
}