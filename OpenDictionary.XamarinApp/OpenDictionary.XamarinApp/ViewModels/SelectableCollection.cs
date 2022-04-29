using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;

using Xamarin.CommunityToolkit.ObjectModel;

namespace OpenDictionary.ViewModels
{
    public delegate IQueryable<TOut> SelectQueryFactory<TIn, TOut>(IQueryable<TIn> query);

    public class SelectableCollection<TStorage, T> : CollectionViewModel<T>
    {
        private readonly IStorage<TStorage> storage;

        public SelectQueryFactory<TStorage, T> Query { get; }

        public SelectableCollection(IStorage<TStorage> storage, SelectQueryFactory<TStorage, T> query, Func<T, Task> tappedCommand)
        {
            this.storage = storage;

            Query = query;

            LoadCommand = new AsyncCommand(Load);

            TappedCommand = new AsyncCommand<T>(tappedCommand);
        }

        public async Task Load()
        {
            IsBusy = true;

            IQueryable<TStorage> storageQuery = storage.Query();

            IQueryable<T> query = Query.Invoke(storageQuery);

            var items = await query.ToArrayAsync();

            Collection.Clear();

            Collection.AddRange(items);

            IsBusy = false;
        }
    }
}