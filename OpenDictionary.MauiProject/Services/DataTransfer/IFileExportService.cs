using System.Threading.Tasks;

namespace OpenDictionary.Services.DataTransfer;

public interface IFileExportService
{
    Task AsSingleFile(string title, string path);
    Task AsMultipleFiles(string title, string[] paths);
}