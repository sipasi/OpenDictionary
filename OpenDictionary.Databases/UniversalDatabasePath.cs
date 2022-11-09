namespace OpenDictionary.Databases;

public sealed class UniversalDatabasePath : IDatabasePath
{
    public string Path { get; }

    public UniversalDatabasePath(string name = "database")
    {
        Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{name}.db");
    }
}