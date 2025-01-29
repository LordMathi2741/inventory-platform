using ApiGateway.Management.Interface.REST.Resources;
using Support.Management.Domain.Model.Aggregates;

namespace ApiGateway.Management.Interface.REST.Transform;

public class ProductResourceFromEntityAssembler
{
    public static ProductResource ToResourceFromEntity(Product product)
    {
        return new ProductResource(
            product.Id.ToString(),
            product.Name,
            product.Description,
            product.Price,
            product.Stock
            );
    }
}