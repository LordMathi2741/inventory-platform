using ApiGateway.Management.Interface.REST.Resources;
using ApiGateway.Management.Interface.REST.Transform;
using Management.Application.RabbitMq.Publishers;
using Management.Domain.Model.Commands;
using Management.Domain.Model.Queries;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ApiGateway.Management;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductGateway : ControllerBase
{
    private readonly ProductPublisher _productPublisher;
    
    public ProductGateway(ProductPublisher productPublisher)
    {
        _productPublisher = productPublisher;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource createProductResource)
    {
        var command = CreateProductCommandFromResourceAssembler.ToCommandFromResource(createProductResource);
        await _productPublisher.CreateProduct(command);
        return StatusCode(201, true);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateProduct([FromQuery] string id, [FromBody] UpdateProductResource updateProductResource)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            return BadRequest("Invalid id");
        }
        var command = UpdateProductCommandFromResourceAssembler.ToCommandFromResource(objectId,updateProductResource);
        await _productPublisher.UpdateProduct(command);
        return Ok(true);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromQuery] string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            return BadRequest("Invalid id");
        }
        var command = new DeleteProductCommand(objectId);
        await _productPublisher.DeleteProduct(command);
        return Ok(true);
    }
    
    [HttpGet("id")]
    public async Task<IActionResult> GetProduct([FromQuery] string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            return BadRequest("Invalid id");
        }
        var query = new GetProductByIdQuery(objectId);
        var product = await _productPublisher.GetProductById(query);
        if (product == null) return NotFound();
        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return Ok(productResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetAllProductsQuery();
        var products = await _productPublisher.GetProducts(query);
        var productResources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(productResources);
    }
}