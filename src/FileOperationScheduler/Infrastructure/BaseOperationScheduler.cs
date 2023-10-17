using System.Reflection;
using FileOperationScheduler.Core;
using FileOperationScheduler.Core.Models;
using FileOperationScheduler.Infrastructure.Operations;

namespace FileOperationScheduler.Infrastructure;

public abstract class BaseOperationScheduler : IOperationScheduler
{
    #region Private members

    private readonly List<OperationConfiguration> _operations = new();

    private static IEnumerable<IOperation> GetOperations(List<OperationConfiguration> configurations)
    {
        return configurations.Select(cfg => cfg.AsOperation()).ToList();
    }

    #endregion

    #region Public methods

    public IOperationScheduler AddOperation(OperationConfiguration operationConfiguration)
    {
        _operations.Add(operationConfiguration);
        return this;
    }

    public async Task ExecutePlanAsync()
    {
        var ops = GetOperations(_operations);
        foreach (var op in ops) await op.ProcessAsync();
    }

    public SchedulerState GetState() { return new SchedulerState { OperationCount = _operations.Count }; }


    public IOperationScheduler ResetPlan()
    {
        _operations.Clear();
        return this;
    }

    public abstract Task SavePlanAsync();

    #endregion

    protected IEnumerable<OperationConfiguration> Operations => _operations.ToArray();

    protected IOperationScheduler AddOperations(IEnumerable<OperationConfiguration> operations, bool resetList = true)
    {
        if (resetList) _operations.Clear();

        _operations.AddRange(operations);
        return this;
    }
}