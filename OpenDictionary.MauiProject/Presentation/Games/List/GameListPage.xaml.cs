using Microsoft.Maui.Controls;

using OpenDictionary.Games.ViewModels;

namespace OpenDictionary.Games.Pages;

public partial class GameListPage : ContentPage
{
    public GameListPage(GameListViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}