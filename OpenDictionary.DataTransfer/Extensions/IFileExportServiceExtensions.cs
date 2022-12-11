using OpenDictionary.DataTransfer.Services;

namespace OpenDictionary.DataTransfer.Extensions;

public static class IFileExportServiceExtensions
{
    public static async ValueTask AsSingleFile<T>(this IFileExportService service, IFileExporter exporter, FileData<T>[] datas)
    {
        using IReadOnlyCacheContainer<IFile> cache = await exporter.AsSingleFile(datas);

        await service.AsSingleFile(cache.Files[0]);
    }
    public static async ValueTask AsMultipleFiles<T>(this IFileExportService service, IFileExporter exporter, FileData<T>[] datas)
    {
        using IReadOnlyCacheContainer<IFile> cache = await exporter.AsMultipleFiles(datas);

        await service.AsMultipleFiles(cache.Files);
    }
}