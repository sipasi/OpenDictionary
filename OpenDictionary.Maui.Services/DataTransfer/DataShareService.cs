using System.Linq;
using System.Threading.Tasks;

using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace OpenDictionary.Services.DataTransfer;

internal sealed class DataShareService : IFileExportService
{
    public Task AsSingleFile(string title, string path)
    {
        var single = new ShareFileRequest
        {
            Title = title,
            File = new ShareFile(path)
        };

        return Share.RequestAsync(single);
    }
    public Task AsMultipleFiles(string title, string[] paths)
    {
        var files = paths.Select(static path => new ShareFile(path)).ToList();

        var multiple = new ShareMultipleFilesRequest
        {
            Title = title,
            Files = files
        };

        return Share.RequestAsync(multiple);
    }
}