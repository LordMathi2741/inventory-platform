namespace ApiGateway.Management.Interface.REST.Resources;

public record UpdateProductResource(string Name, string Description, decimal Price, int Stock);