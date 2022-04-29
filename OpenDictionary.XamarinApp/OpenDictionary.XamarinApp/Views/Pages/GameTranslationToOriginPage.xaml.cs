using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels.Games;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameTranslationToOriginPage : ContentPage
    {
        private readonly TranslationToOriginViewModel viewModel;

        public GameTranslationToOriginPage()
        {
            InitializeComponent();

            BindingContext = viewModel = DiContainer.Get<TranslationToOriginViewModel>();
        }
    }
}