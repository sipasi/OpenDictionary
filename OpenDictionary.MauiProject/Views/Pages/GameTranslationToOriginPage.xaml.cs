using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels.Games;

namespace OpenDictionary.Views.Pages;

public partial class GameTranslationToOriginPage : ContentPage
{
    public GameTranslationToOriginPage()
    {
        InitializeComponent();

        BindingContext = DiContainer.Get<TranslationToOriginViewModel>();
    }
}