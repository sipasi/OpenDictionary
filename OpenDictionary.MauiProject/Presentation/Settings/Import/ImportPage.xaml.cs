using Microsoft.Maui.Controls;

using OpenDictionary.Settings.ViewModels;

namespace OpenDictionary.Settings.Pages;

public partial class ImportPage : ContentPage
{
    public ImportPage(WordGroupImportViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}