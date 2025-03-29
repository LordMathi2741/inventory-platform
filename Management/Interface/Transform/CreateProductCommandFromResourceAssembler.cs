using ApiGateway.Management.Interface.REST.Resources;
using Management.Domain.Model.Commands;

namespace ApiGateway.Management.Interface.REST.Transform;

public class CreateProductCommandFromResourceAssembler
{
    public static CreateProductCommand ToCommandFromResource(CreateProductResource createProductResource)
    {
        return new CreateProductCommand(
            createProductResource.Name,
            createProductResource.Description,
            createProductResource.Price,
            createProductResource.Stock
            );
    }
}