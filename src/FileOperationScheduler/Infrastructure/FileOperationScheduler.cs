using FileOperationScheduler.Core;
using FileOperationScheduler.Core.Models;
using Newtonsoft.Json;
using System.Collections.Immutable;

namespace FileOperationScheduler.Infrastructure;

internal class FileOperationScheduler : BaseOperationScheduler
{
    private readonly string _fullName;

    public FileOperationScheduler(string fullName) => _fullName = fullName;

    public async Task LoadFileAsync()
    {
        var file       = !File.Exists(_fullName) ? File.Create(_fullName) : File.OpenRead(_fullName);
        var operations = new List<IOperationConfiguration>();

        await using (file)
        using (var reader = new StreamReader(file))
        {
            var json = await reader.ReadToEndAsync();
            var op =
                JsonConvert.DeserializeObject<List<OperationConfiguration>>(json)
                ?? new List<OperationConfiguration>();

            operations.AddRange(op);

            AddOperations(operations);
        }
    }

    public override async Task SavePlanAsync()
    {
        var json = JsonConvert.SerializeObject(Operations);
        await File.WriteAllTextAsync(_fullName, json);
    }
}