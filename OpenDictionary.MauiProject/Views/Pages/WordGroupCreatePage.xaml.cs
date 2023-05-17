using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;

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