using System.IO.Compression;
using FileOperationScheduler.Core;

namespace FileOperationScheduler.Infrastructure.Operations;

[Operation("unzip")]
internal class UnzipOperation : BaseOperation, IOperation
{
    #region Private members

    private string ArchiveFile => Parameters["zip"];

    private string Destination => Parameters["destination"];

    #endregion

    #region Constructors

    public UnzipOperation(Dictionary<string, string> parameters) : base("unzip", parameters) { }

    #endregion

    #region Public methods

    public async Task ProcessAsync()
    {
        if (!File.Exists(ArchiveFile)) return;

        await Task.Run(() => ZipFile.ExtractToDirectory(ArchiveFile, Destination));
    }

    #endregion
}