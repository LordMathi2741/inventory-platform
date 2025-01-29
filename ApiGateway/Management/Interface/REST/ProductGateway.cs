using ApiGateway.Management.Interface.REST.Resources;
using ApiGateway.Management.Interface.REST.Transform;
using Management.Interface.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Support.Management.Domain.Model.Commands;

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
}