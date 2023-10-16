namespace FileOperationScheduler.Core;

public interface IOperationScheduler
{
    #region Public methods

    IOperationScheduler AddOperation(IOperationConfiguration operationConfiguration);
    Task ExecutePlanAsync();

    SchedulerState GetState();

    IOperationScheduler ResetPlan();

    Task SavePlanAsync();

    #endregion
}