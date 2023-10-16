namespace FileOperationScheduler.Core;

public interface IOperationScheduler
{
    IOperationScheduler AddOperation(IOperationConfiguration operationConfiguration);

    Task SavePlanAsync();

    SchedulerState GetState();

    IOperationScheduler ResetPlan();
    Task ExecutePlanAsync();
}