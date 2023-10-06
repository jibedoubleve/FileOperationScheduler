using System.Collections;
using System.Reflection;
using FileOperationScheduler.Core;
using FileOperationScheduler.Infrastructure.Operations;

namespace FileOperationScheduler.Infrastructure;

public abstract class BaseOperationScheduler : IOperationScheduler
{
    private readonly List<IOperationConfiguration> _operations = new();

    protected IEnumerable<IOperationConfiguration> Operations => _operations.ToArray();

    public IOperationScheduler AddOperation(IOperationConfiguration operationConfiguration)
    {
        _operations.Add(operationConfiguration);
        return this;
    }

    protected IOperationScheduler AddOperations(IEnumerable<IOperationConfiguration> operations, bool resetList = true)
    {
        if (resetList) _operations.Clear();

        _operations.AddRange(operations);
        return this;
    }

    public abstract Task SavePlanAsync();

    public SchedulerState GetState() => new() { OperationCount = _operations.Count };


    public IOperationScheduler ResetPlan()
    {
        _operations.Clear();
        return this;
    }

    public async Task ExecutePlanAsync()
    {
        var ops = GetOperations(_operations);
        foreach (var op in ops)
        {
            await op.ProcessAsync();
        }
    }

    private static readonly IEnumerable<Type> Types =
        Assembly.GetAssembly(typeof(BaseOperation))?.GetTypes()
        ?? Type.EmptyTypes;

    private static IEnumerable<IOperation> GetOperations(List<IOperationConfiguration> operations)
    {
        var ops = new List<IOperation>();

        foreach (var operation in operations)
        {
            var type =
                (from t in Types
                 where t.GetCustomAttributes<OperationAttribute>(true)
                        .Any(x => x.Name == operation.Name)
                 select t).FirstOrDefault();

            if (type is null) throw new NotSupportedException($"Cannot find operation '{operation.Name}'");

            var o = (IOperation)Activator.CreateInstance(type, args: operation.Parameters)!;
            ops.Add(o);
        }

        return ops;
    }
}