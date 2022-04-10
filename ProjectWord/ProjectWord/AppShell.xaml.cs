using ProjectWord.Views.Pages;

using Xamarin.Forms;

namespace ProjectWord
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(WordGroupCreatePage), typeof(WordGroupCreatePage));
            Routing.RegisterRoute(nameof(WordGroupDetailPage), typeof(WordGroupDetailPage));
            Routing.RegisterRoute(nameof(WordEditPage), typeof(WordEditPage));
        }
    }
}
