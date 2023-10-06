namespace FileOperationScheduler.Core;

public interface IOperation
{
    string? Name { get; set; }
    string? Parameters { get; set; }
}