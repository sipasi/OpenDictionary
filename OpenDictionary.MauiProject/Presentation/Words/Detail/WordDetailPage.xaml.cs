using Microsoft.Maui.Controls;

using OpenDictionary.Words.ViewModels;

namespace OpenDictionary.Words.Pages;

public partial class WordDetailPage : ContentPage
{
    public WordDetailPage(WordDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}