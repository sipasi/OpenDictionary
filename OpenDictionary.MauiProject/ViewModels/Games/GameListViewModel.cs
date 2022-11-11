using Microsoft.Maui.Controls;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;

namespace OpenDictionary.ViewModels.Games;

[QueryProperty(nameof(Id), nameof(Id))]
public class GameListViewModel : OpenDictionary.Games.WordConformities.ViewModels.GameListViewModel
{
    public GameListViewModel(IStorage<WordGroup> storage, INavigationService navigation)
       : base(storage, navigation, new(AppRoutes.Game.OriginToTranslation, AppRoutes.Game.TranslationToOrigin)) { }
}