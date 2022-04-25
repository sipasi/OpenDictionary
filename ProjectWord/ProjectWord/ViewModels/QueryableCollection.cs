using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;

using Xamarin.CommunityToolkit.ObjectModel;

namespace OpenDictionary.ViewModels
{
    public delegate IQueryable<T> QueryFactory<T>(IQueryable<T> query);

    public class QueryableCollection<T> : CollectionViewModel<T>
    {
        private readonly IStorage<T> storage;

        public QueryFactory<T> Query { get; }

        public QueryableCollection(IStorage<T> storage, QueryFactory<T> query, Func<T, Task> tappedCommand)
        {
            this.storage = storage;

            Query = query;

            LoadCommand = new AsyncCommand(Load);

            TappedCommand = new AsyncCommand<T>(tappedCommand);
        }

        public async Task Load()
        {
            IsBusy = true;

            IQueryable<T> query = storage.Query();

            query = Query.Invoke(query);

            var items = await query.ToArrayAsync();

            Collection.Clear();

            Collection.AddRange(items);

            IsBusy = false;
        }
    }
}