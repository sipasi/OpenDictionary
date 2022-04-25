
using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordDetailPage : ContentPage
    {
        public WordDetailPage()
        {
            InitializeComponent();

            BindingContext = DiContainer.Get<WordDetailViewModel>();
        }
    }
}