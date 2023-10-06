namespace FileOperationScheduler.Core.Models;

public class OperationConfiguration : IOperationConfiguration
{
    public string Name { get; set; } = "noop"; 
    public Dictionary<string, string> Parameters { get; set; } = new();
}