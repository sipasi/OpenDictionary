using Microsoft.Maui.Controls;


using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages.Settings;

public partial class ExportPage : ContentPage
{
    public ExportPage(ExportViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

        viewModel.LoadCommand.Execute(null);
    }
}