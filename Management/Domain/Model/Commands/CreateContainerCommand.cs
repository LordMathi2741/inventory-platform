namespace Management.Domain.Model.Commands;

public record CreateContainerCommand(string Name, string Description, int Capacity, int ProductId);