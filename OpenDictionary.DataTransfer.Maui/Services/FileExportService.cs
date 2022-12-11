using System.Linq;

using OpenDictionary.DataTransfer;
using OpenDictionary.DataTransfer.Services;

namespace OpenDictionary.Services.DataTransfer;

internal sealed class FileExportService : IFileExportService
{
    public async ValueTask AsSingleFile(IFileSource file)
    {
        var single = new ShareFileRequest
        {
            Title = file.Name,
            File = new ShareFile(file.FullPath)
        };

        await Share.RequestAsync(single);
    }
    public async ValueTask AsMultipleFiles(IReadOnlyList<IFileSource> sources)
    {
        var files = sources.Select(static file => new ShareFile(file.FullPath)).ToList();

        var multiple = new ShareMultipleFilesRequest
        {
            Title = "Files",
            Files = files
        };

        await Share.RequestAsync(multiple);
    }
}
