using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure.Operations;

[Operation("mvdir")]
internal class MoveDirectoryOperation : BaseOperation, IOperation
{
    public MoveDirectoryOperation(Dictionary<string, string> parameters) : base("mvdir",parameters)
    {
    }

    public Task ProcessAsync()
    {
        var src = Parameters["source"];
        var dst = Parameters["destination"];

        if (!Directory.Exists(src)) return Task.CompletedTask;

        Directory.Move(src, dst);
        return Task.CompletedTask;
    }
}