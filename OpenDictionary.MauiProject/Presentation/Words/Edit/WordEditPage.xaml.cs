using Microsoft.Maui.Controls;

using OpenDictionary.Words.ViewModels;

namespace OpenDictionary.Words.Pages;

public partial class WordEditPage : ContentPage
{
    public WordEditPage(WordEditViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}