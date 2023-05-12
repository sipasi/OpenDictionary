namespace OpenDictionary.Databases;

public class AppDatabaseConnection(IDatabasePath path) : DatabaseConnection<AppDatabaseContext>
{
    public override AppDatabaseContext Open() => new(path);
}