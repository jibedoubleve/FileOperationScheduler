using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure;

public static class OperationSchedulerFactory
{
    public static IOperationScheduler RetrieveFromMemory() => new MemoryOperationScheduler();

    public static IOperationScheduler RetrieveFromFile(string filePath) => new FileOperationScheduler(filePath);

    public static IOperationScheduler RetrieveFromTempFile()
    {
            var path = Path.Combine(Path.GetTempPath(), "lanceur_operation_log.json");
            return RetrieveFromFile(path);
    }
}