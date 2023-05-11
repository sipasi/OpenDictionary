namespace OpenDictionary.Databases;

public interface IDatabasePath
{
    string Path { get; }
}

public record DatabasePath : IDatabasePath
{
    public required string Path { get; init; }
}