namespace FileOperationScheduler;

public class OperationAttribute : Attribute
{
    #region Constructors

    public OperationAttribute(string name) { Name = name; }

    #endregion

    #region Public properties

    public string Name { get; }

    #endregion
}