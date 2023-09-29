namespace FileOperationScheduler.Core.Models;

public class OperationLog : IOperation
{
    public string? Name { get; set; }
    public string? Parameters { get; set; }
}