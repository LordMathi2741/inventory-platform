
using Management.Domain.Service;
using Support.Management.Domain.Model.Commands;

namespace Management.Interface.RabbitMQ;

public class InventoryController 
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly IProductCommandService _productCommandService;

    public InventoryController(IRabbitMqService rabbitMqService, IProductCommandService productCommandService)
    {
        _rabbitMqService = rabbitMqService;
        _productCommandService = productCommandService;
    }
  
    
    public async Task CreateProduct(CreateProductCommand command)
    {
        await _productCommandService.Handle(command);
        await _rabbitMqService.SendMessageAsync(command);
    }
    
    public async Task UpdateProduct( UpdateProductCommand command)
    {
        await _productCommandService.Handle(command);
        await _rabbitMqService.SendMessageAsync(command);
    }
    
    public async Task DeleteProduct(DeleteProductCommand command)
    {
        await _productCommandService.Handle(command);
        await _rabbitMqService.SendMessageAsync(command);
        
    }
}
