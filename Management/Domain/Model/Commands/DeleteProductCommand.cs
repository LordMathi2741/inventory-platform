using MongoDB.Bson;

namespace Management.Domain.Model.Commands;

public record DeleteProductCommand(ObjectId Id);