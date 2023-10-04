using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure;

public abstract class BaseOperationScheduler : IOperationScheduler
{
    private static readonly List<IOperation> s_operations = new();

    protected IEnumerable<IOperation> Operations => s_operations.ToArray();

    private static bool s_isCommitted;

    public IOperationScheduler AddOperation(IOperation operation1)
    {
        s_operations.Add(operation1);
        return this;
    }

    public IOperationScheduler SavePlan()
    {
        CommitImpl();
        s_isCommitted = true;
        return this;
    }

    protected abstract void CommitImpl();

    public SchedulerState GetState() => new() { OperationCount = s_isCommitted ? s_operations.Count : 0 };

    protected IOperationScheduler Reset(IEnumerable<IOperation> operations)
    {
        s_operations.Clear();
        s_operations.AddRange(operations);
        return this;
    }

    public IOperationScheduler ResetPlan()
    {
        s_operations.Clear();
        return this;
    }
}