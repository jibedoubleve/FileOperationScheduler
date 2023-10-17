using FileOperationScheduler.Infrastructure.Operations;
using FluentAssertions;

namespace FileOperationScheduler.Test.SystemTests.Operations;

public class RemoveDirectoryOperationShould
{
    #region Public methods

    [Fact]
    public async Task BeProcessed()
    {
        // ARRANGE
        var directory = Path.Combine(Path.GetTempPath(), "RandomDirectory");
        Directory.CreateDirectory(directory);


        // ACT
        var rmdir = OperationFactory.RemoveDirectory(directory);
        Directory.Exists(directory).Should().BeTrue();

        await rmdir.ProcessAsync();

        // ASSERT
        Directory.Exists(directory).Should().BeFalse();
    }

    #endregion
}