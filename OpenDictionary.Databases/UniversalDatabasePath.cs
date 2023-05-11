namespace OpenDictionary.Databases;

public sealed record UniversalDatabasePath : DatabasePath
{
    public UniversalDatabasePath(string name = "database")
    {
        Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"{name}.db");
    }
}