using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels.Games;

namespace OpenDictionary.Views.Pages;

public partial class GameListPage : ContentPage
{
    public GameListPage()
    {
        InitializeComponent();

        BindingContext = DiContainer.Get<GameListViewModel>();
    }
}