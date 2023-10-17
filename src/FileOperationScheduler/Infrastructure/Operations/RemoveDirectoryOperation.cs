using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure.Operations;

[Operation("rmdir")]
internal class RemoveDirectoryOperation : BaseOperation, IOperation
{
    #region Constructors

    public RemoveDirectoryOperation(Dictionary<string, string> parameters) : base("rmdir", parameters) { }

    #endregion

    #region Public methods

    public Task ProcessAsync()
    {
        var directory = Parameters["directory"];
        if (!Directory.Exists(directory)) return Task.CompletedTask;

        Directory.Delete(directory, true);
        return Task.CompletedTask;
    }

    #endregion
}