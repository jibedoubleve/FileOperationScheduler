using System.IO.Compression;
using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure.Operations;

[Operation("unzip")]
internal class UnzipOperation : BaseOperation, IOperation
{
    public UnzipOperation(Dictionary<string, string> parameters) : base("unzip", parameters)
    {
    }

    public async Task ProcessAsync()
    {
        if (!File.Exists(ArchiveFile)) return;

        await Task.Run(() => ZipFile.ExtractToDirectory(ArchiveFile, Destination));
    }

    private string Destination => Parameters["destination"];
    private string ArchiveFile => Parameters["zip"];
}