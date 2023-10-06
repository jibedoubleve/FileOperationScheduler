using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure;

public abstract class BaseOperationScheduler : IOperationScheduler
{
    private readonly List<IOperation> _operations = new();

    protected IEnumerable<IOperation> Operations => _operations.ToArray();

    public IOperationScheduler AddOperation(IOperation operation)
    {
        _operations.Add(operation);
        return this;
    }

    public abstract Task SavePlanAsync();

    public SchedulerState GetState() => new() { OperationCount =  _operations.Count };

    protected IOperationScheduler SetOperations(IEnumerable<IOperation> operations)
    {
        _operations.Clear();
        _operations.AddRange(operations);
        return this;
    }

    public IOperationScheduler ResetPlan()
    {
        _operations.Clear();
        return this;
    }
}