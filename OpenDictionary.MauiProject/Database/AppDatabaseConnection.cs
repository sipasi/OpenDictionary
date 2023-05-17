namespace OpenDictionary.Databases;

public class AppDatabaseConnection : DatabaseConnection<AppDatabaseContext>
{
    private readonly IDatabasePath path;

    public AppDatabaseConnection(IDatabasePath path) => this.path = path;

    public override AppDatabaseContext Open() => new(path);
}