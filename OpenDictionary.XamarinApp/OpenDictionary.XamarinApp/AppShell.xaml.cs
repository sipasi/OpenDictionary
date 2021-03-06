using OpenDictionary.Services.Navigations.Routes;
using OpenDictionary.Views.Pages;

using Xamarin.Forms;

namespace OpenDictionary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(AppRoutes.WordGroup.Create, typeof(WordGroupCreatePage));
            Routing.RegisterRoute(AppRoutes.WordGroup.Detail, typeof(WordGroupDetailPage));
            Routing.RegisterRoute(AppRoutes.Word.Detail, typeof(WordDetailPage));
            Routing.RegisterRoute(AppRoutes.Word.Edit, typeof(WordEditPage));

            Routing.RegisterRoute(AppRoutes.Game.List, typeof(GameListPage));
            Routing.RegisterRoute(AppRoutes.Game.OriginToTranslation, typeof(GameOriginToTranslationPage));
            Routing.RegisterRoute(AppRoutes.Game.TranslationToOrigin, typeof(GameTranslationToOriginPage));

            tabBar.CurrentItem = defautTab;
        }
    }
}
