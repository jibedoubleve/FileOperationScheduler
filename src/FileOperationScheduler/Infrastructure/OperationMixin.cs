using System.Reflection;
using FileOperationScheduler.Core;
using FileOperationScheduler.Core.Models;
using FileOperationScheduler.Infrastructure.Operations;

namespace FileOperationScheduler.Infrastructure;

public static class OperationMixin
{
    private static readonly IEnumerable<Type> Types =
        Assembly.GetAssembly(typeof(BaseOperation))?.GetTypes()
        ?? Type.EmptyTypes;
    
    public static IOperation AsOperation(this OperationConfiguration cfg)
    {
        var type =
            (from t in Types
             where t.FullName == cfg.Name
             select t).FirstOrDefault();

        if (type is null) throw new NotSupportedException($"Cannot find operation '{cfg.Name}'");

        return (IOperation)Activator.CreateInstance(type, cfg.Parameters)!;
    }
}