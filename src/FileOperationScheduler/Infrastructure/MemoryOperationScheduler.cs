using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure;

internal class MemoryOperationScheduler : BaseOperationScheduler
{
    public override Task SavePlanAsync() => Task.CompletedTask;
}