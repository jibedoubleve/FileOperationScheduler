namespace FileOperationScheduler.Core;
public interface IFileOperationScheduler : IFileOperation
{
    void Register(IFileOperation operation);
}
