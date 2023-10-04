namespace FileOperationScheduler.Core;

public interface IOperationScheduler
{
    IOperationScheduler AddOperation(IOperation operation);

    IOperationScheduler SavePlan();

    SchedulerState GetState();

    IOperationScheduler ResetPlan();
}