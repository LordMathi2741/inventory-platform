using Support.Management.Domain.Model.Commands;
using Support.Management.Domain.Model.Queries;

namespace Management.Domain.Service;

public interface IRabbitMqService
{
    Task SendMessageAsync(CreateProductCommand command);
    Task SendMessageAsync(UpdateProductCommand command);
    Task SendMessageAsync(DeleteProductCommand command);
    
    Task SendMessageAsync(GetAllProductsQuery query);
    Task SendMessageAsync(GetProductByIdQuery query);
    
}