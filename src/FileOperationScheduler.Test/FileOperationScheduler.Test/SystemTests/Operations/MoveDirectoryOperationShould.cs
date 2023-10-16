using FileOperationScheduler.Infrastructure.Operations;
using FluentAssertions;

namespace FileOperationScheduler.Test.SystemTests.Operations;

public class MoveDirectoryOperationShould : IDisposable
{
    private const string SourceName      = "RandomDirectory_src_MVDIR";
    private const string DestinationName = "RandomDirectory_dst_MVDIR";

    public MoveDirectoryOperationShould()
    {
        Source = Path.Combine(Path.GetTempPath(), SourceName);

        Cleanup();
        Directory.CreateDirectory(Source);

        using (var fileStream = File.Create(Path.Combine(Source, "output.txt")))
        using (var writer = new StreamWriter(fileStream))
        {
            writer.WriteLine("some random text");
        }

        Destination = Path.Combine(Path.GetTempPath(), DestinationName);
    }

    private string Source { get;  }

    private string Destination { get;  }

    [Fact]
    public async Task BeProcessed()
    {
        // ACT
        var moveDirectory = OperationFactory.MoveDirectory(Source, Destination);
        await moveDirectory.ProcessAsync();

        // ASSERT
        Directory.Exists(Destination)
                 .Should().BeTrue($"'{Destination}' should be created");

        Directory.EnumerateFiles(Destination)
                 .Count()
                 .Should().BeGreaterThan(0);
    }

    public void Dispose() => Cleanup();

    private void Cleanup()
    {
        if (Directory.Exists(Source)) Directory.Delete(Source, true);
        if (Directory.Exists(Destination)) Directory.Delete(Destination, true);
    }
}