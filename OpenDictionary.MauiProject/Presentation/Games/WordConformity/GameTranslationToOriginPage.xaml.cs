using Microsoft.Maui.Controls;

using OpenDictionary.Games.WordConformities.ViewModels;

namespace OpenDictionary.Games.WordConformities.Pages;

public partial class GameTranslationToOriginPage : ContentPage
{
    public GameTranslationToOriginPage(TranslationToOrigin viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}