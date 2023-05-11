namespace OpenDictionary.DataTransfer;

public readonly struct FileData<T>
{
    public required string Name { get; init; }
    public required T Data { get; init; }
}