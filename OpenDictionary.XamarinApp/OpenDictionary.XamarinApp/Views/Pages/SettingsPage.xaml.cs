using OpenDictionary.DependencyInjection;
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

            BindingContext = viewModel = DiContainer.Get<SettingsViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.AppTheme.UpdateRadioButtonsCommand.Execute(radioGroup);
        }
    }
}