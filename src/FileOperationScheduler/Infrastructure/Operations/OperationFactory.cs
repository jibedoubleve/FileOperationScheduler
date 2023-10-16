using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure.Operations;

public static class OperationFactory
{
    public static IOperation RemoveDirectory(string directory)
    {
        var parameters = new Dictionary<string, string>()
        {
            { "directory", directory }
        };
        return new RemoveDirectoryOperation(parameters);
    }

    public static IOperation MoveDirectory(string source, string destination)
    {
        var parameters = new Dictionary<string, string>()
        {
            { "source", source },
            { "destination", destination }
        };
        return new MoveDirectoryOperation(parameters);
    }

    public static IOperation UnzipDirectory(string zipFile, string destination)
    {
        var parameters = new Dictionary<string, string>()
        {
            { "zip", zipFile },
            { "destination", destination }
        };
        return new UnzipOperation(parameters);
    }
}