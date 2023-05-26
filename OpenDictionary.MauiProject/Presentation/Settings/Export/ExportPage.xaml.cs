using Microsoft.Maui.Controls;

using OpenDictionary.Settings.ViewModels;

namespace OpenDictionary.Settings.Pages;

public partial class ExportPage : ContentPage
{
    public ExportPage(WordGroupExportViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;

        viewModel.LoadCommand.Execute(null);
    }
}