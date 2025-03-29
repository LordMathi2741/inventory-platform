using MongoDB.Bson;

namespace Management.Domain.Model.Queries;

public record GetProductByIdQuery(ObjectId Id);