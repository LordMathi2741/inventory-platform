using Management.Domain.Model.Aggregates;
using Management.Domain.Model.Commands;
using Management.Domain.Model.Queries;
using Management.Domain.Service;
using Management.Infrastructure.RabbitMQ;

namespace Management.Application.RabbitMq.Publishers;

public class ProductPublisher
{
    private readonly IRabbitMqService _rabbitMqService;
    private readonly IProductCommandService _productCommandService;
    private readonly IProductQueryService _productQueryService;

    public ProductPublisher(IRabbitMqService rabbitMqService, IProductCommandService productCommandService, IProductQueryService productQueryService)
    {
        _rabbitMqService = rabbitMqService;
        _productCommandService = productCommandService;
        _productQueryService = productQueryService;
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
    
    public async Task<Product?> GetProductById(GetProductByIdQuery query)
    {
        await _rabbitMqService.SendMessageAsync(query);
        return await _productQueryService.Handle(query);
        
    }
    
    public async Task<IEnumerable<Product>> GetProducts(GetAllProductsQuery query)
    {
        await _rabbitMqService.SendMessageAsync(query);
        return await _productQueryService.Handle(query);
    }
}
