using System.Collections.Generic;

using Microsoft.Maui;
using Microsoft.Maui.Controls;

using OpenDictionary.ViewModels.Settings;

namespace OpenDictionary.Views.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly SettingsViewModel viewModel;

    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = this.viewModel = viewModel;


    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        IList<IView> container = radioGroup;

        viewModel.AppTheme.UpdateRadioButtonsCommand.Execute(container);
    }
}