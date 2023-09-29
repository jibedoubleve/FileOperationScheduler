namespace FileOperationScheduler.Core;
public interface IOperationScheduler : IOperation
{
    void Register(IOperation operation);
}
