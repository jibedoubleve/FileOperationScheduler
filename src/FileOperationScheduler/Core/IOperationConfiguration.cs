namespace FileOperationScheduler.Core;

public interface IOperationConfiguration
{
    #region Public properties

    string Name { get; }

    Dictionary<string, string> Parameters { get; }

    #endregion
}