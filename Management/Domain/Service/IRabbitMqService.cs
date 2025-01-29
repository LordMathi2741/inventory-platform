using Support.Management.Domain.Model.Commands;

namespace Management.Domain.Service;

public interface IRabbitMqService
{
    Task SendMessageAsync(CreateProductCommand command);
    Task SendMessageAsync(UpdateProductCommand command);
    Task SendMessageAsync(DeleteProductCommand command);
    
}