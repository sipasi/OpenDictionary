using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordGroupDetailPage : ContentPage
    {
        public WordGroupDetailPage()
        {
            InitializeComponent();

            BindingContext = DiContainer.Get<WordGroupDetailViewModel>();
        }
    }
}