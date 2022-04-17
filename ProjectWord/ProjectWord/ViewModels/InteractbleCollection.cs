
using System;
using System.Threading.Tasks;

using Xamarin.CommunityToolkit.ObjectModel;

namespace OpenDictionary.ViewModels
{
    public class InteractbleCollection<T>
    {
        public ObservableRangeCollection<T> Collection { get; }

        public AsyncCommand<T> TappedCommand { get; }


        public InteractbleCollection(Func<T, Task> tappedCommand)
        {
            Collection = new ObservableRangeCollection<T>();

            TappedCommand = new AsyncCommand<T>(tappedCommand);
        }
    }
}
