using ProjectWord.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectWord.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordGroupDetailPage : ContentPage
    {
        private readonly WordGroupDetailViewModel viewModel;

        public WordGroupDetailPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new WordGroupDetailViewModel();
        }
    }
}