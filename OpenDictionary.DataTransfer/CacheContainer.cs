using System.Diagnostics.CodeAnalysis;

using OpenDictionary.DataTransfer.Extensions;

namespace OpenDictionary.DataTransfer;

public interface IReadOnlyCacheContainer<out T> : IDisposable where T : IFileSource, IFileOperation
{
    public IReadOnlyList<T> Files { get; }
}

public sealed class CacheContainer<T> : IReadOnlyCacheContainer<T> where T : IFileSource, IFileOperation
{
    public delegate T CreateFactory(string path, string name);

    private readonly List<T> files = new();

    public required string Path { get; init; }
    public required CreateFactory Factory { get; init; }

    public CacheContainer() { }

    [SetsRequiredMembers]
    public CacheContainer(string path, CreateFactory factory)
    {
        Path = path;
        Factory = factory;
    }

    public IReadOnlyList<T> Files => files;

    public T Create(string name)
    {
        T file = Factory.Invoke(Path, name);

        files.Add(file);

        return file;
    }

    public void Dispose()
    {
        foreach (var file in files)
        {
            file.DeleteIfExists();
        }

        files.Clear();
    }
}