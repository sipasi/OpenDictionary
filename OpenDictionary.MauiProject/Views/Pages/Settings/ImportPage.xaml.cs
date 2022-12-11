using Microsoft.Maui.Controls;

using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages.Settings;

public partial class ImportPage : ContentPage
{
    public ImportPage(WordGroupImportViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}