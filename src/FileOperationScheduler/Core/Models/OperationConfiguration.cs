namespace FileOperationScheduler.Core.Models;

public class OperationConfiguration
{
    #region Public properties

    public string? Name { get; init; }
    public Dictionary<string, string> Parameters { get; set; } = new();

    #endregion
}