using Microsoft.Maui.Controls;

using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages;

public partial class WordDetailPage : ContentPage
{
    public WordDetailPage(WordDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}