using MongoDB.Bson;
using Support.Management.Domain.Model.Commands;

namespace Support.Management.Domain.Model.Aggregates;

public partial class Container
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Capacity { get; set; }
    public int ProductId { get; set; }
}

public partial class Container
{
    public Container()
    {
        Name = string.Empty;
        Description = string.Empty;
        Capacity = 0;
        ProductId = 0;
    }

    public Container(CreateContainerCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        Capacity = command.Capacity;
        ProductId = command.ProductId;
    }
}