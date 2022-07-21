using System;
using System.Linq;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;

namespace OpenDictionary.ViewModels;

public delegate IQueryable<T> QueryFactory<T>(IQueryable<T> query);

public class QueryableCollection<T> : CollectionViewModel<T>
{
    private readonly IStorage<T> storage;

    public QueryFactory<T> Query { get; }

    public QueryableCollection(IStorage<T> storage, QueryFactory<T> query, Func<T?, Task> tappedCommand)
    {
        this.storage = storage;

        Query = query;

        LoadCommand = new(Load);

        TappedCommand = new(tappedCommand);
    }

    public async Task Load()
    {
        IsBusy = true;

        IQueryable<T> query = storage.Query();

        query = Query.Invoke(query);

        var items = await Task.Run(query.ToArray);

        Collection.Clear();

        Collection.AddRange(items);

        IsBusy = false;
    }
}