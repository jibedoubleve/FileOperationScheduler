namespace FileOperationScheduler.Infrastructure.Operations;

public abstract class BaseOperation
{
    #region Constructors

    internal BaseOperation(string name, Dictionary<string, string> parameters)
    {
        Name = name;
        Parameters = parameters;
    }

    #endregion

    #region Public properties

    public string Name { get; init; }

    public Dictionary<string, string> Parameters { get; init; }

    #endregion
}