using ApiGateway.Management.Interface.REST.Resources;
using ApiGateway.Management.Interface.REST.Transform;
using Management.Interface.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Support.Management.Domain.Model.Commands;
using Support.Management.Domain.Model.Queries;

namespace ApiGateway.Management.Interface.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductGateway : ControllerBase
{
    private readonly InventoryController _inventoryController;

    public ProductGateway(InventoryController inventoryController)
    {
        _inventoryController = inventoryController;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource createProductResource)
    {
        var command = CreateProductCommandFromResourceAssembler.ToCommandFromResource(createProductResource);
        await _inventoryController.CreateProduct(command);
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
        await _inventoryController.UpdateProduct(command);
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
        await _inventoryController.DeleteProduct(command);
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
        var product = await _inventoryController.GetProductById(query);
        if (product == null) return NotFound();
        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return Ok(productResource);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetAllProductsQuery();
        var products = await _inventoryController.GetProducts(query);
        var productResources = products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(productResources);
    }
}