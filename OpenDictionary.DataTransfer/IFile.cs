namespace OpenDictionary.DataTransfer;

public interface IFile : IFileSource, IFileOperation { }

public interface IFileSource
{
    string Path { get; init; }
    string Name { get; init; }
    string FullPath { get; }
}

public interface IFileOperation
{
    ValueTask Write<T>(T data);
    ValueTask<T?> Read<T>();
}