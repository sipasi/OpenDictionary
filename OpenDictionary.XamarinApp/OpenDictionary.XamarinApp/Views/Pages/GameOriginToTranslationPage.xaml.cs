using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels.Games;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameOriginToTranslationPage : ContentPage
    {
        private readonly OriginToTranslationViewModel viewModel;

        public GameOriginToTranslationPage()
        {
            InitializeComponent();

            BindingContext = viewModel = DiContainer.Get<OriginToTranslationViewModel>();
        }
    }
}