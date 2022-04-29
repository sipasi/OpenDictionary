using System;
using System.Threading.Tasks;

using Xamarin.CommunityToolkit.ObjectModel;

namespace OpenDictionary.ViewModels
{
    public class CollectionViewModel<T> : ViewModel
    {
        private bool isBusy;
        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }

        public ObservableRangeCollection<T> Collection { get; }

        public AsyncCommand LoadCommand { get; protected set; }

        public AsyncCommand<T> TappedCommand { get; protected set; }

        public CollectionViewModel()
        {
            Collection = new ObservableRangeCollection<T>();
        }
        public CollectionViewModel(Func<Task> loadCommand, Func<T, Task> tappedCommand) : this()
        {
            LoadCommand = new AsyncCommand(loadCommand);
            TappedCommand = new AsyncCommand<T>(tappedCommand);
        }
    }
}