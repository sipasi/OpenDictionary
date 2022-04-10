
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace ProjectWord.ViewModels
{
    public abstract class CollectionViewModel<T> : StorageViewModel
    {
        public ObservableCollection<T> Items { get; }

        public Command LoadItemsCommand { get; }
        public Command<T> ItemTapped { get; }

        public CollectionViewModel()
        {
            Items = new ObservableCollection<T>();

            LoadItemsCommand = new Command(async () => await LoadItems());

            ItemTapped = new Command<T>(OnItemSelected);
        }

        public virtual void OnAppearing()
        {
            IsBusy = true;
        }

        protected abstract void OnItemSelected(T item);
    }
}
