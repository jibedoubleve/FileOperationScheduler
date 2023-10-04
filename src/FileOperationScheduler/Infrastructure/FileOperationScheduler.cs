using FileOperationScheduler.Core;
using FileOperationScheduler.Core.Models;
using Newtonsoft.Json;
using System.Collections.Immutable;

namespace FileOperationScheduler.Infrastructure;

internal class FileOperationScheduler : BaseOperationScheduler
{
    private readonly string _fullName;

    public FileOperationScheduler(string fullName)
    {
        _fullName = fullName;
    }

    protected override void CommitImpl()
    {
        var file = (!File.Exists(_fullName)) ? File.Create(_fullName) : File.OpenRead(_fullName);

        using (file)
        using (var reader = new StreamReader(file))
        {
            var json = reader.ReadToEnd();
            var op =
                JsonConvert.DeserializeObject<List<OperationLog>>(json)
                ?? new List<OperationLog>();

            var operations = new List<IOperation>(op);
            operations.AddRange(Operations);

            Reset(operations);
        }
    }
}