using FileOperationScheduler.Core.Models;
using Newtonsoft.Json;

namespace FileOperationScheduler.Infrastructure;

internal class FileOperationScheduler : BaseOperationScheduler
{
    #region Private members

    private readonly string _fullName;

    #endregion

    #region Constructors

    public FileOperationScheduler(string fullName) { _fullName = fullName; }

    #endregion

    #region Public methods

    public async Task LoadFileAsync()
    {
        var file = !File.Exists(_fullName) ? File.Create(_fullName) : File.OpenRead(_fullName);
        var operations = new List<OperationConfiguration>();

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

    #endregion
}