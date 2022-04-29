using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordGroupCreatePage : ContentPage
    {
        public WordGroupCreatePage()
        {
            InitializeComponent();

            BindingContext = DiContainer.Get<WordGroupEditViewModel>();
        }
    }
}