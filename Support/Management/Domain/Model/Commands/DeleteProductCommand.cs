using MongoDB.Bson;

namespace Support.Management.Domain.Model.Commands;

public record DeleteProductCommand(ObjectId Id);