
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.CommunityToolkit.ObjectModel;

namespace OpenDictionary.ViewModels
{
    public abstract class CollectionViewModel<T> : ViewModel
    {
        private bool isBusy;

        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }

        public ObservableRangeCollection<T> Items { get; }

        public AsyncCommand LoadItemsCommand { get; }
        public AsyncCommand<T> ItemTapped { get; }

        public CollectionViewModel()
        {
            Items = new ObservableRangeCollection<T>();

            LoadItemsCommand = new AsyncCommand(LoadItemsAsync);

            ItemTapped = new AsyncCommand<T>(OnItemSelected);
        }

        public virtual async void OnAppearing()
        {
            await LoadItemsAsync();
        }

        private async Task LoadItemsAsync()
        {
            IsBusy = true;

            try
            {
                Items.Clear();

                IEnumerable<T> items = await Load();

                Items.AddRange(items);
            }
            catch (System.Exception)
            {
                return;
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected abstract Task<IEnumerable<T>> Load();

        protected abstract Task OnItemSelected(T item);
    }
}