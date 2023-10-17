namespace FileOperationScheduler.Core;

public interface IOperation : IOperationConfiguration
{
    #region Public methods

    Task ProcessAsync();

    #endregion
}