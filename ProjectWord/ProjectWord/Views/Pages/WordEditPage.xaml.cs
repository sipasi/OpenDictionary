using ProjectWord.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectWord.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordEditPage : ContentPage
    {
        public WordEditPage()
        {
            InitializeComponent();

            BindingContext = new WordEditViewModel();
        }
    }
}