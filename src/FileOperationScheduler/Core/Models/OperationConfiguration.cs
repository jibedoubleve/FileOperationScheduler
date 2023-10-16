namespace FileOperationScheduler.Core.Models;

public class OperationConfiguration : IOperationConfiguration
{
    #region Public properties

    public string Name { get; set; } = "noop";
    public Dictionary<string, string> Parameters { get; set; } = new();

    #endregion
}