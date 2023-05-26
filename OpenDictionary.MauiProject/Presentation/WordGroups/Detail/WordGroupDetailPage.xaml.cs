using Microsoft.Maui.Controls;

using OpenDictionary.WordGroups.ViewModels;

namespace OpenDictionary.Views.Pages;

public partial class WordGroupDetailPage : ContentPage
{
    public WordGroupDetailPage(WordGroupDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}