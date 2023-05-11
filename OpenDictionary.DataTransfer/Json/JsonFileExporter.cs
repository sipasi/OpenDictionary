using System.Text.Json;

namespace OpenDictionary.DataTransfer.Json;

public sealed class JsonFileExporter : IFileExporter
{
    private readonly CacheContainer<JsonFile>.CreateFactory factory;

    public required string CacheDirectory { get; init; }
    public required JsonSerializerOptions Options { get; init; }

    public JsonFileExporter()
    {
        factory = CreateFile;
    }

    public async ValueTask<IReadOnlyCacheContainer<IFile>> AsSingleFile<T>(FileData<T>[] infos)
    {
        CacheContainer<JsonFile> cache = new(CacheDirectory, factory);

        IFile file = cache.Create("Files");

        IEnumerable<T> datas = infos.Select(data => data.Data);

        await file.Write(datas);

        return cache;
    }

    public async ValueTask<IReadOnlyCacheContainer<IFile>> AsMultipleFiles<T>(FileData<T>[] datas)
    {
        CacheContainer<JsonFile> cache = new(CacheDirectory, factory);

        foreach (var data in datas)
        {
            var file = cache.Create(data.Name);

            await file.Write(data.Data);
        }

        return cache;
    }

    private JsonFile CreateFile(string path, string name) => new()
    {
        Path = path,
        Name = name,
        Options = Options
    };
}