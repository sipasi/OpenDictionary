using OpenDictionary.Views.Pages;

using Xamarin.Forms;

namespace OpenDictionary
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(WordGroupCreatePage), typeof(WordGroupCreatePage));
            Routing.RegisterRoute(nameof(WordGroupDetailPage), typeof(WordGroupDetailPage));
            Routing.RegisterRoute(nameof(WordDetailPage), typeof(WordDetailPage));
            Routing.RegisterRoute(nameof(WordEditPage), typeof(WordEditPage));

            Routing.RegisterRoute(nameof(GameOriginToTranslationPage), typeof(GameOriginToTranslationPage));
            Routing.RegisterRoute(nameof(GameTranslationToOriginPage), typeof(GameTranslationToOriginPage));

            tabBar.CurrentItem = defautTab;
        }
    }
}
