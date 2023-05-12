using Microsoft.EntityFrameworkCore;

namespace OpenDictionary.Databases;

public interface IDatabaseConnection<out T> where T : DbContext
{
    T Open();

    void Open(Action<T> operation);

    TResult Open<TResult>(Func<T, TResult> operation);
    ValueTask<TResult> Open<TResult>(Func<T, Task<TResult>> operation);
}

public abstract class DatabaseConnection<T> : IDatabaseConnection<T>
    where T : DbContext
{
    public abstract T Open();

    public void Open(Action<T> operation)
    {
        using T context = Open();

        operation.Invoke(context);
    }

    public TResult Open<TResult>(Func<T, TResult> operation)
    {
        using T context = Open();

        TResult result = operation.Invoke(context);

        return result;
    }

    public async ValueTask<TResult> Open<TResult>(Func<T, Task<TResult>> operation)
    {
        await using T context = Open();

        TResult result = await operation.Invoke(context);

        return result;
    }
}