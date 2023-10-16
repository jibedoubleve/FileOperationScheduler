using FileOperationScheduler.Infrastructure.Operations;
using FileOperationScheduler.Test.Helpers;
using FluentAssertions;
using Xunit.Abstractions;

namespace FileOperationScheduler.Test.SystemTests.Operations;

public class UnzipDirectoryOperationShould : IDisposable
{
    private const string ArchiveFileName = "Package.zip";
    private const string SourceDirectoryName = "RandomDirectory_src_ZIP";
    private const string DestinationDirectoryName = "RandomDirectory_dst_ZIP";
    private const string TextFileName = "random_text_file.txt";

    public UnzipDirectoryOperationShould(ITestOutputHelper output)
    {
        Source      = Path.Combine(Path.GetTempPath(), SourceDirectoryName);
        Destination = Path.Combine(Path.GetTempPath(), DestinationDirectoryName);
        ArchiveFile = Path.Combine(Path.GetTempPath(), Destination, ArchiveFileName);

        Cleanup();

        Directory.CreateDirectory(Source);
        Directory.CreateDirectory(Destination);

        var outfile = Path.Combine(Source, TextFileName);
        
        output.WriteLine($"Source dir     : '{Source}'");
        output.WriteLine($"Destination dir: '{Destination}'");
        output.WriteLine($"Archive dir    : '{ArchiveFile}'");

        ZipHelper.Zip(outfile, ArchiveFile);
    }

    [Fact]
    public async Task BeProcessed()
    {
        // ACT
        var unzipDir = OperationFactory.UnzipDirectory(ArchiveFile, Destination);
        await unzipDir.ProcessAsync();

        // ASSERT
        var path = Path.Combine(Destination, TextFileName);
        Directory.EnumerateFiles(Destination)
                 .Count(f => f == path)
                 .Should().BeGreaterThan(0);
    }

    private string Source { get;  }
    private string ArchiveFile { get; }
    private string Destination { get;  }

    private void Cleanup()
    {
        if (Directory.Exists(Source)) Directory.Delete(Source, true);
        if (Directory.Exists(Destination)) Directory.Delete(Destination, true);
        if (File.Exists(ArchiveFile)) File.Delete(ArchiveFile);
    }

    public void Dispose() => Cleanup();
}