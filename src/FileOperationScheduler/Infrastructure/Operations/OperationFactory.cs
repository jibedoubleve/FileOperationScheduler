using FileOperationScheduler.Core.Models;

namespace FileOperationScheduler.Infrastructure.Operations;

public static class OperationFactory
{
    #region Public methods

    public static OperationConfiguration MoveDirectory(string source, string destination)
    {
        var parameters = new Dictionary<string, string>
        {
            { "source", source },
            { "destination", destination }
        };
        return new OperationConfiguration
        {
            Parameters = parameters,
            Name = typeof(MoveDirectoryOperation).FullName!
        };
    }

    public static OperationConfiguration RemoveDirectory(string directory)
    {
        var parameters = new Dictionary<string, string>
        {
            { "directory", directory }
        };
        return new OperationConfiguration
        {
            Name = typeof(RemoveDirectoryOperation).FullName!,
            Parameters = parameters
        };
    }

    public static OperationConfiguration UnzipDirectory(string zipFile, string destination)
    {
        var parameters = new Dictionary<string, string>
        {
            { "zip", zipFile },
            { "destination", destination }
        };
        return new OperationConfiguration
        {
            Parameters = parameters,
            Name = typeof(UnzipOperation).FullName!
        };
    }

    #endregion
}