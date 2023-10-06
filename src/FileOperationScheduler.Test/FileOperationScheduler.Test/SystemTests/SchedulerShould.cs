using System.Reflection.PortableExecutable;
using FileOperationScheduler.Core;
using FileOperationScheduler.Core.Models;
using FileOperationScheduler.Infrastructure;
using FileOperationScheduler.Test.Helpers;
using FluentAssertions;
using Moq;

namespace FileOperationScheduler.Test.SystemTests;

public class SchedulerShould : IDisposable
{
    private const string FilePattern = "lanceur_operation_log_{0}.json";
    private readonly string _fileName = string.Format(FilePattern, Guid.NewGuid());

    private static List<IOperationConfiguration> GetRandomOperations(int count)
    {
        var results = new List<IOperationConfiguration>();
        for (var i = 0; i < count; i++)
            results.Add(
                new OperationConfiguration() { Name = $"NoOperation_{i}", Parameters = new() { { "1", "un" } } }
            );
        return results;
    }

    [Fact]
    public async Task CreateInMemoryBeforeSaving()
    {
        // ARRANGE
        var scheduler  = await OperationSchedulerFactory.RetrieveFromFileAsync(_fileName);
        var operations = GetRandomOperations(4);

        var i = 0;

        // ACT
        scheduler.ResetPlan()
                 .AddOperation(operations[i++])
                 .AddOperation(operations[i++])
                 .AddOperation(operations[i++]);

        // ASSERT
        scheduler.GetState()
                 .OperationCount
                 .Should()
                 .Be(3);
    }

    [Fact]
    public async Task RetrievePreviouslySavedScheduler()
    {
        // ARRANGE
        var scheduler  = await OperationSchedulerFactory.RetrieveFromFileAsync(_fileName);
        var operations = GetRandomOperations(4);

        var i = 0;

        // ACT
        await scheduler.ResetPlan()
                       .AddOperation(operations[i++])
                       .AddOperation(operations[i++])
                       .AddOperation(operations[i++])
                       .SavePlanAsync();

        // ASSERT
        var retrieved = await OperationSchedulerFactory.RetrieveFromFileAsync(_fileName);
        retrieved.GetState()
                 .OperationCount
                 .Should()
                 .Be(3);
    }

    [Fact]
    public async Task RetrievePreviouslyAndAppendOperation()
    {
        // ARRANGE
        var scheduler  = await OperationSchedulerFactory.RetrieveFromFileAsync(_fileName);
        var operations = GetRandomOperations(4);

        var i = 0;

        // ACT
        await scheduler.ResetPlan()
                       .AddOperation(operations[i++])
                       .AddOperation(operations[i++])
                       .AddOperation(operations[i++])
                       .SavePlanAsync();

        var scheduler2 = await OperationSchedulerFactory.RetrieveFromFileAsync(_fileName);
        await scheduler2.AddOperation(operations[i++])
                        .SavePlanAsync();

        var retrieved = await OperationSchedulerFactory.RetrieveFromFileAsync(_fileName);

        // ASSERT
        retrieved.GetState()
                 .OperationCount
                 .Should()
                 .Be(4);
    }

    public void Dispose()
    {
        if (!File.Exists(_fileName)) return;

        File.Delete(_fileName);
    }
}