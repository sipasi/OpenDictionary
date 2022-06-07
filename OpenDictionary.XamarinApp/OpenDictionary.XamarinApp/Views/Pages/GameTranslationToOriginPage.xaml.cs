using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels.Games;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameTranslationToOriginPage : ContentPage
    {
        public GameTranslationToOriginPage()
        {
            InitializeComponent();

            BindingContext = DiContainer.Get<TranslationToOriginViewModel>();
        }
    }
}