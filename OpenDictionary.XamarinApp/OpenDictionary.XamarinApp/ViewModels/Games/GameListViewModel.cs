using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;

using Xamarin.Forms;

namespace OpenDictionary.ViewModels.Games
{
    [QueryProperty(nameof(Id), nameof(Id))]
    internal class GameListViewModel : OpenDictionary.Games.WordConformities.ViewModels.GameListViewModel
    {
        public GameListViewModel(IStorage<WordGroup> storage, INavigationService navigation)
            : base(storage, navigation) { }
    }
}