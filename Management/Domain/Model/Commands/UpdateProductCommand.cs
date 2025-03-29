using MongoDB.Bson;

namespace Management.Domain.Model.Commands;

public record UpdateProductCommand(ObjectId Id, string Name, string Description, decimal Price, int Stock);