using OpenDictionary.DataTransfer.Services;
using OpenDictionary.Services.DataTransfer;

namespace OpenDictionary.DataTransfer;

public static class ServiceBuilderExtensions
{
    public static IServiceCollection ConfigureDataTransfer(this IServiceCollection services, IFileExporter exporter)
    {
        services.AddSingleton<IFileExporter>(exporter);

#if WINDOWS
        services.AddSingleton<IFileExportService, FileExportServiceWindows>();
#else
        services.AddSingleton<IFileExportService, FileExportService>();
#endif

        return services;
    }
}
