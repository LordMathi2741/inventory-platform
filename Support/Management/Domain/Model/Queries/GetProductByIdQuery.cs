using MongoDB.Bson;

namespace Support.Management.Domain.Model.Queries;

public record GetProductByIdQuery(ObjectId Id);