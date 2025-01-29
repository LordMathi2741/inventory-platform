using MongoDB.Bson;

namespace Support.Management.Domain.Model.Commands;

public record UpdateProductCommand(ObjectId Id, string Name, string Description, decimal Price, int Stock);