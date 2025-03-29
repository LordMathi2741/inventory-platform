using Management.Domain.Model.Commands;
using Management.Domain.Model.Queries;

namespace Management.Infrastructure.RabbitMQ;

public interface IRabbitMqService
{
    Task SendMessageAsync(CreateProductCommand command);
    Task SendMessageAsync(UpdateProductCommand command);
    Task SendMessageAsync(DeleteProductCommand command);
    
    Task SendMessageAsync(GetAllProductsQuery query);
    Task SendMessageAsync(GetProductByIdQuery query);
    
}