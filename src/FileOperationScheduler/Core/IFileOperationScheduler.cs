namespace Core;
public interface IFileOperationScheduler : IFileOperation
{
    void Register(IFileOperation operation);
}
