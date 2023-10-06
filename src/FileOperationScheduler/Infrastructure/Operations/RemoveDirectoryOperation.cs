using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure.Operations;

[Operation("rmdir")]
internal class RemoveDirectoryOperation : BaseOperation, IOperation
{
    public RemoveDirectoryOperation(Dictionary<string, string> parameters) : base("rmdir", parameters)
    {
    }

    public Task ProcessAsync()
    {
        var directory = Parameters["directory"];
        if (!Directory.Exists(directory)) return Task.CompletedTask;

        Directory.Delete(directory, true);
        return Task.CompletedTask;
    }
}