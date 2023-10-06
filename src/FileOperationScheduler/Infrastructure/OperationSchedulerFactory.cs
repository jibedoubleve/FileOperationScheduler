using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure;

public static class OperationSchedulerFactory
{
    public static IOperationScheduler RetrieveFromMemory() => new MemoryOperationScheduler();

    public static async Task<IOperationScheduler> RetrieveFromFileAsync(string filePath)
    {
        var scheduler =   new FileOperationScheduler(filePath);
        await scheduler.LoadFileAsync();
        return scheduler;
    }
}