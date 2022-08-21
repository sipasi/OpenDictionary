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

        viewModel.LoadCommand.Execute(null);
    }
}