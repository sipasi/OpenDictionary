using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels.Games;

namespace OpenDictionary.Views.Pages;

public partial class GameOriginToTranslationPage : ContentPage
{
    public GameOriginToTranslationPage()
    {
        InitializeComponent();

        BindingContext = DiContainer.Get<OriginToTranslationViewModel>();
    }
}