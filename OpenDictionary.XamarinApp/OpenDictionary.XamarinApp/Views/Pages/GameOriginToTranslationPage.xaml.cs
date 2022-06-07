using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels.Games;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameOriginToTranslationPage : ContentPage
    {
        public GameOriginToTranslationPage()
        {
            InitializeComponent();

            BindingContext = DiContainer.Get<OriginToTranslationViewModel>();
        }
    }
}