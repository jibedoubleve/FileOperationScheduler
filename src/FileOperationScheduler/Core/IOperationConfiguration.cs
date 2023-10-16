namespace FileOperationScheduler.Core;

public interface IOperationConfiguration
{
    Dictionary<string, string>  Parameters { get;  }
    
    string Name { get; }
}