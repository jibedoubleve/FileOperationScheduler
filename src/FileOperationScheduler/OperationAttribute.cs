namespace FileOperationScheduler;

public class OperationAttribute : Attribute
{
    public string Name { get; }

    public OperationAttribute(string name)
    {
        Name = name;
    }
}