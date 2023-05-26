using Microsoft.Maui.Controls;

using OpenDictionary.Games.WordConformities.ViewModels;

namespace OpenDictionary.Games.WordConformities.Pages;

public partial class GameOriginToTranslationPage : ContentPage
{
    public GameOriginToTranslationPage(OriginToTranslation viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}