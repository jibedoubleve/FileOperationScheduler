namespace FileOperationScheduler.Infrastructure;

internal class MemoryOperationScheduler : BaseOperationScheduler
{
    #region Public methods

    public override Task SavePlanAsync() { return Task.CompletedTask; }

    #endregion
}