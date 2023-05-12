using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Databases;

namespace OpenDictionary.ViewModels;

public delegate IQueryable<TOut> QueryFactory<TIn, TOut>(IQueryable<TIn> query);

public class QueryableCollection<T> : QueryableCollection<T, T>
    where T : class
{
    public QueryableCollection(IDatabaseConnection<DbContext> connection, QueryFactory<T, T> query, Func<T?, Task> tappedCommand)
        : base(connection, query, tappedCommand) { }
}

public class QueryableCollection<TStorage, TElement> : CollectionViewModel<TElement>
    where TStorage : class
{
    private readonly IDatabaseConnection<DbContext> connection;

    public QueryFactory<TStorage, TElement> Query { get; }

    public QueryableCollection(IDatabaseConnection<DbContext> connection, QueryFactory<TStorage, TElement> query, Func<TElement?, Task> tappedCommand)
    {
        this.connection = connection;

        Query = query;

        LoadCommand = new(Load);

        TappedCommand = new(tappedCommand);
    }

    public async Task Load()
    {
        IsBusy = true;

        await using DbContext context = connection.Open();

        IQueryable<TElement> query = Query.Invoke(context.Set<TStorage>());

        TElement[]? items = default;

        try
        {
            items = await query.ToArrayAsync();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);

            return;
        }

        Collection.Clear();

        Collection.AddRange(items);

        IsBusy = false;
    }
}