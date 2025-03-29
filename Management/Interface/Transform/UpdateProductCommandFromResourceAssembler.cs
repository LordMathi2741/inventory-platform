using ApiGateway.Management.Interface.REST.Resources;
using Management.Domain.Model.Commands;
using MongoDB.Bson;

namespace ApiGateway.Management.Interface.REST.Transform;

public class UpdateProductCommandFromResourceAssembler
{
    public static UpdateProductCommand ToCommandFromResource(ObjectId id, UpdateProductResource resource)
    {
        return new UpdateProductCommand(
            id,
            resource.Name,
            resource.Description,
            resource.Price,
            resource.Stock
        );
    }
}