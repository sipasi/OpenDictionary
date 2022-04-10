using ProjectWord.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectWord.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WordGroupListPage : ContentPage
    {
        private readonly WordGroupListViewModel viewModel;

        public WordGroupListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new WordGroupListViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.OnAppearing();
        }
    }
}