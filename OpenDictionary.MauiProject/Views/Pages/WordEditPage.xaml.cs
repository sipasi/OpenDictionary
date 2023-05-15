using Microsoft.Maui.Controls;

using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages;

public partial class WordEditPage : ContentPage
{
    public WordEditPage(WordEditViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}