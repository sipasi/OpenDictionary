using System.Collections.Generic;
using System.Threading.Tasks;

using OpenDictionary.Services.Navigations;
using OpenDictionary.Views.Pages;

using Xamarin.CommunityToolkit.ObjectModel;

namespace OpenDictionary.ViewModels.Games
{
    public class GameListViewModel : ViewModel
    {
        private readonly INavigationService navigation;

        public ICollection<GameInfo> Collection { get; }

        public AsyncCommand<GameInfo> TappedCommand { get; }

        public GameListViewModel(INavigationService navigation)
        {
            this.navigation = navigation;

            TappedCommand = new AsyncCommand<GameInfo>(OnTapped);

            Collection = new List<GameInfo>()
            {
                new GameInfo
                {
                    Image = "icon_origin_to_translation.png",
                    Name = "Origin to translation",
                    Description = "",
                    Page = typeof(GameOriginToTranslationPage),
                },
                new GameInfo
                {
                    Image = "icon_translation_to_origin.png",
                    Name = "Translation to origin",
                    Description = "",
                    Page = typeof(GameTranslationToOriginPage),
                },
            };
        }

        private Task OnTapped(GameInfo info)
        {
            return navigation.GoToAsync(info.Page.Name);
        }
    }
}