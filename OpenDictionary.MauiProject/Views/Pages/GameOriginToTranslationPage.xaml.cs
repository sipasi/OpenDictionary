using Microsoft.Maui.Controls;


using OpenDictionary.ViewModels.Games;

namespace OpenDictionary.Views.Pages;

public partial class GameOriginToTranslationPage : ContentPage
{
    public GameOriginToTranslationPage(OriginToTranslationViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}