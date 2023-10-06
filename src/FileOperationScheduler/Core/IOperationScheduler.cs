namespace FileOperationScheduler.Core;

public interface IOperationScheduler
{
    IOperationScheduler AddOperation(IOperation operation);

    Task SavePlanAsync();

    SchedulerState GetState();

    IOperationScheduler ResetPlan();
}