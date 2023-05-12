namespace OpenDictionary.Databases;

public interface IDatabasePath
{
    string Path { get; }
}

public record DatabasePath : IDatabasePath
{
    public required string Path { get; init; }

    public static DatabasePath Create(string name)
    {
        string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        return new()
        {
            Path = System.IO.Path.Combine(folder, $"{name}.db")
        };
    }
}