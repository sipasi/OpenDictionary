using Microsoft.Maui.Controls;

using OpenDictionary.ViewModels.Games;

namespace OpenDictionary.Views.Pages;

public partial class GameTranslationToOriginPage : ContentPage
{
    public GameTranslationToOriginPage(TranslationToOriginViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}