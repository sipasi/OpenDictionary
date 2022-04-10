using ProjectWord.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectWord.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordGroupCreatePage : ContentPage
    {
        private readonly WordGroupEditViewModel viewModel;

        public WordGroupCreatePage()
        {
            InitializeComponent();

            BindingContext = viewModel = new WordGroupEditViewModel();
        }
    }
}