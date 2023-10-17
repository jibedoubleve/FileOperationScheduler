using FileOperationScheduler.Core;
using FileOperationScheduler.Infrastructure;
using FluentAssertions;
using Moq;

namespace FileOperationScheduler.Test.UnitTests;

public class SchedulerShould
{
    #region Public methods

    [Fact]
    public async Task CreateOperation_WhenSaved()
    {
        // ARRANGE
        var operation1 = new Mock<IOperationConfiguration>();
        var operation2 = new Mock<IOperationConfiguration>();
        var operation3 = new Mock<IOperationConfiguration>();

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

    #endregion
}