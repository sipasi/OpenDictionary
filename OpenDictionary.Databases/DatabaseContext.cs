using Microsoft.EntityFrameworkCore;

namespace OpenDictionary.Databases;

public abstract class DatabaseContextBase : DbContext
{
    private readonly string databasePath;

    public DatabaseContextBase(string databasePath)
    {
        this.databasePath = databasePath;
    }

    protected sealed override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite($"Filename={databasePath}");

        builder.EnableSensitiveDataLogging(true);
    }
}