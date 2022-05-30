using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages;

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