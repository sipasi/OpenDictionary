namespace OpenDictionary.Databases;

internal abstract class AppDatabaseStorage<T> : DatabaseStorage<T> where T : class
{
    protected AppDatabaseStorage(IDatabasePath path) : base(path) { }

    protected sealed override DatabaseContextBase Open(IDatabasePath path) => new DatabaseContext(path);

    protected DatabaseContext Cast(DatabaseContextBase context) => (context as DatabaseContext)!;
}
