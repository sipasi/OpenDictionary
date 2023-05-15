using Microsoft.Maui.Controls;

using OpenDictionary.ViewModels.WordGroups;

namespace OpenDictionary.Views.Pages;

public partial class WordGroupDetailPage : ContentPage
{
    public WordGroupDetailPage(WordGroupDetailViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}