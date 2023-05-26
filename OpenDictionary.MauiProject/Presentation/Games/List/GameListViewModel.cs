using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

using OpenDictionary.Databases;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;

namespace OpenDictionary.Games.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public class GameListViewModel : WordConformities.ViewModels.GameListViewModel
{
    public GameListViewModel(IDatabaseConnection<DbContext> connection, INavigationService navigation)
       : base(connection, navigation, new(AppRoutes.Game.OriginToTranslation, AppRoutes.Game.TranslationToOrigin)) { }
}