using Microsoft.Maui.Controls;

using OpenDictionary.ViewModels.WordGroups;

namespace OpenDictionary.Views.Pages;

public partial class WordGroupCreatePage : ContentPage
{
    public WordGroupCreatePage(WordGroupEditViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}