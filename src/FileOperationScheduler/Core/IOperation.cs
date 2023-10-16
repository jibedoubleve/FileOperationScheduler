namespace FileOperationScheduler.Core;

public interface IOperation : IOperationConfiguration
{
    Task ProcessAsync();
}