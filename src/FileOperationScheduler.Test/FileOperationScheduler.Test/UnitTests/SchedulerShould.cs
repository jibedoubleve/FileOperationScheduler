using FileOperationScheduler.Core;
using FileOperationScheduler.Infrastructure;
using FluentAssertions;
using Moq;

namespace FileOperationScheduler.Test.UnitTests;

public class SchedulerShould
{
    [Fact]
    public async Task CreateOperation_WhenSaved()
    {
        // ARRANGE
        var operation1 = new Mock<IOperation>();
        var operation2 = new Mock<IOperation>();
        var operation3 = new Mock<IOperation>();

        // ACT
        var scheduler = OperationSchedulerFactory.RetrieveFromMemory();
        await scheduler.ResetPlan()
                       .AddOperation(operation1.Object)
                       .AddOperation(operation2.Object)
                       .AddOperation(operation3.Object)
                       .SavePlanAsync();

        // ASSERT
        scheduler.GetState()
                 .OperationCount
                 .Should().Be(3);
    }
}