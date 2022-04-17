using OpenDictionary.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenDictionary.Views.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsViewModel viewModel;

        public SettingsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new SettingsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.UpdateRadioButtonsGroup(radioGroup);
        }
    }
}