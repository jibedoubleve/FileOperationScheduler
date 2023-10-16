namespace FileOperationScheduler.Infrastructure.Operations;

public abstract class BaseOperation
{
    public string Name { get; init; }

    internal BaseOperation(string name, Dictionary<string, string> parameters)
    {
        Name = name;
        Parameters = parameters;
    }

    public Dictionary<string, string> Parameters { get; init; }
}