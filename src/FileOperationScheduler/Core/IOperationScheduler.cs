using FileOperationScheduler.Core.Models;

namespace FileOperationScheduler.Core;

public interface IOperationScheduler
{
    #region Public methods

    IOperationScheduler AddOperation(OperationConfiguration operationConfiguration);
    Task ExecutePlanAsync();

    SchedulerState GetState();

    IOperationScheduler ResetPlan();

    Task SavePlanAsync();

    #endregion
}