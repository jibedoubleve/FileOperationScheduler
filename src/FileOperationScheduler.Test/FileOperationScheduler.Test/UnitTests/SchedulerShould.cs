using FileOperationScheduler.Core;
using FileOperationScheduler.Infrastructure;
using FluentAssertions;
using Moq;

namespace FileOperationScheduler.Test;

public class SchedulerShould
{
    [Fact]
    public void CreateOperation_WhenCommitted()
    {
        // ARRANGE
        var scheduler = OperationSchedulerFactory.RetrieveFromMemory();
        var operation1 = new Mock<IOperation>();
        var operation2 = new Mock<IOperation>();
        var operation3 = new Mock<IOperation>();

        // ACT
        var state = scheduler.ResetPlan()
                             .AddOperation(operation1.Object)
                             .AddOperation(operation2.Object)
                             .AddOperation(operation3.Object)
                             .SavePlan()
                             .GetState();

        // ASSERT
        state.OperationCount.Should()
             .Be(3);
    }

    [Fact]
    public void DoNotCreateOperation_WhenNotCommitted()
    {
        // ARRANGE
        var scheduler = OperationSchedulerFactory.RetrieveFromMemory();
        var operation1 = new Mock<IOperation>();
        var operation2 = new Mock<IOperation>();
        var operation3 = new Mock<IOperation>();

        // ACT
        var state = scheduler.ResetPlan()
                             .AddOperation(operation1.Object)
                             .AddOperation(operation2.Object)
                             .AddOperation(operation3.Object)
                             .GetState();

        // ASSERT
        state.OperationCount.Should()
             .Be(0);
    }
}