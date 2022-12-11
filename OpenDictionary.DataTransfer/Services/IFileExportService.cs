namespace OpenDictionary.DataTransfer.Services;

public interface IFileExportService
{
    ValueTask AsSingleFile(IFileSource source);
    ValueTask AsMultipleFiles(IReadOnlyList<IFileSource> sources);
}