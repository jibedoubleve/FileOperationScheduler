namespace FileOperationScheduler.Core;
public interface IOperation
{
    Task ExecuteAsync();
}
