using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;

namespace OpenDictionary.ViewModels;

public delegate IQueryable<TOut> QueryFactory<TIn, TOut>(IQueryable<TIn> query);

public class QueryableCollection<T> : QueryableCollection<T, T>
{
    public QueryableCollection(IStorage<T> storage, QueryFactory<T, T> query, Func<T?, Task> tappedCommand)
        : base(storage, query, tappedCommand) { }
}

public class QueryableCollection<TStorage, TElement> : CollectionViewModel<TElement>
{
    private readonly IStorage<TStorage> storage;

    public QueryFactory<TStorage, TElement> Query { get; }

    public QueryableCollection(IStorage<TStorage> storage, QueryFactory<TStorage, TElement> query, Func<TElement?, Task> tappedCommand)
    {
        this.storage = storage;

        Query = query;

        LoadCommand = new(Load);

        TappedCommand = new(tappedCommand);
    }

    public async Task Load()
    {
        IsBusy = true;

        IQueryable<TElement> query = Query.Invoke(storage.Query());

        TElement[]? items = default;

        try
        {
            items = await Task.Run(query.ToArray);
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