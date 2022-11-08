using System.Diagnostics;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;

namespace OpenDictionary.Databases;

public abstract class DatabaseStorage<TEntity> : IStorage<TEntity> where TEntity : class
{
    private readonly string path;

    public DatabaseStorage(IDatabasePath path)
    {
        this.path = path.Path;
    }

    public async ValueTask<bool> AddAsync(TEntity item)
    {
        try
        {
            Open(out DatabaseContextBase database, out DbSet<TEntity> context);

            var entity = await context.AddAsync(item);

            await database.SaveChangesAsync();
        }
        catch (Exception e)
        {
            WritenException(e);

            return false;
        }

        return true;
    }
    public async ValueTask<bool> AddRangeAsync(IEnumerable<TEntity> items)
    {
        try
        {
            Open(out DatabaseContextBase database, out DbSet<TEntity> context);

            await context.AddRangeAsync(items);

            await database.SaveChangesAsync();
        }
        catch (Exception e)
        {
            WritenException(e);

            return false;
        }

        return true;
    }
    public async ValueTask<bool> DeleteAsync(TEntity item)
    {
        try
        {
            Open(out DatabaseContextBase database, out DbSet<TEntity> context);

            var entity = context.Remove(item);

            await database.SaveChangesAsync();
        }
        catch (Exception e)
        {
            WritenException(e);

            return false;
        }

        return true;
    }

    public async ValueTask<bool> DeleteRangeAsync(IEnumerable<TEntity> items)
    {
        try
        {
            Open(out DatabaseContextBase database, out DbSet<TEntity> context);

            context.RemoveRange(items);

            await database.SaveChangesAsync();
        }
        catch (Exception e)
        {
            WritenException(e);

            return false;
        }

        return true;
    }

    public async ValueTask<bool> UpdateAsync(TEntity item)
    {
        try
        {
            Open(out DatabaseContextBase database, out DbSet<TEntity> context);

            var entity = context.Update(item);

            await database.SaveChangesAsync();
        }
        catch (Exception e)
        {
            WritenException(e);

            return false;
        }

        return true;
    }

    public bool Any()
    {
        Open(out DatabaseContextBase _, out DbSet<TEntity> context);

        return context.Any();
    }

    public IQueryable<TEntity> Query()
    {
        Open(out DatabaseContextBase _, out DbSet<TEntity> context);

        IQueryable<TEntity> query = context.AsNoTracking();

        return query;
    }

    private void Open(out DatabaseContextBase database, out DbSet<TEntity> context)
    {
        database = Open(path);
        context = GetContext(database);
    }

    protected abstract DbSet<TEntity> GetContext(DatabaseContextBase context);

    protected abstract DatabaseContextBase Open(string path);

    private static void WritenException(Exception exception)
    {
        string message = $"Database exception.\nException: {exception.Message}.\nInner: {exception.InnerException?.Message ?? "Empty"}";

        Debug.WriteLine(message);
    }
}