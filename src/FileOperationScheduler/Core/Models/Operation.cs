namespace FileOperationScheduler.Core.Models;

public class Operation : IOperation
{
    public string? Name { get; set; }
    public string? Parameters { get; set; }
}