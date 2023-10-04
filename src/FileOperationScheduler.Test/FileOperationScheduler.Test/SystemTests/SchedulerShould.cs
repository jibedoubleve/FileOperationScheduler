using FileOperationScheduler.Core;
using FileOperationScheduler.Infrastructure;
using FluentAssertions;
using Moq;

namespace FileOperationScheduler.Test.SystemTests;

public class SchedulerShould
{
    [Fact]
    public void CreateSchedulerFromTempFile()
    {
        // ARRANGE
        var scheduler = OperationSchedulerFactory.RetrieveFromTempFile();
        var operations = new List<Mock<IOperation>>() { new(), new(), new() };

        var i = 0;

        // ACT
        var state = scheduler.AddOperation(operations[i++].Object)
                             .AddOperation(operations[i++].Object)
                             .AddOperation(operations[i++].Object)
                             .SavePlan()
                             .GetState();

        // ASSERT
        state.OperationCount
             .Should()
             .Be(3);
    }

    [Fact]
    public void RetrievePreviouslySavedScheduler()
    {
        // ARRANGE
        var scheduler = OperationSchedulerFactory.RetrieveFromTempFile();
        var operations = new List<Mock<IOperation>>() { new(), new(), new() };

        var i = 0;

        // ACT
        scheduler.ResetPlan()
                 .AddOperation(operations[i++].Object)
                 .AddOperation(operations[i++].Object)
                 .AddOperation(operations[i++].Object)
                 .SavePlan()
                 .GetState();
        
        // ASSERT
        var retrieved = OperationSchedulerFactory.RetrieveFromTempFile();
        retrieved.GetState()
                 .OperationCount
                 .Should()
                 .Be(3);
    } 
    
    [Fact]
    public void RetrievePreviouslyAndAppendOperation()
    {
        // ARRANGE
        var scheduler = OperationSchedulerFactory.RetrieveFromTempFile();
        var operations = new List<Mock<IOperation>>() { new(), new(), new(), new() };

        var i = 0;

        // ACT
        scheduler.ResetPlan()
                 .AddOperation(operations[i++].Object)
                 .AddOperation(operations[i++].Object)
                 .AddOperation(operations[i++].Object)
                 .SavePlan()
                 .GetState();
        
        var scheduler2 = OperationSchedulerFactory.RetrieveFromTempFile();
        scheduler2.AddOperation(operations[i++].Object);

        var retrieved = OperationSchedulerFactory.RetrieveFromTempFile();
        
        // ASSERT
        retrieved.GetState()
                 .OperationCount
                 .Should()
                 .Be(4);
    }
}