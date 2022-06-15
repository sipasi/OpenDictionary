using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages.Settings;

public partial class ExportPage : ContentPage
{
    private readonly ExportViewModel viewModel;

    public ExportPage()
    {
        InitializeComponent();

        BindingContext = viewModel = DiContainer.Get<ExportViewModel>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.LoadCommand.Execute(null);
    }
}