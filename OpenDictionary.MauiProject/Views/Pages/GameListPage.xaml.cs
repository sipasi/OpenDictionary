using Microsoft.Maui.Controls;


using OpenDictionary.ViewModels.Games;

namespace OpenDictionary.Views.Pages;

public partial class GameListPage : ContentPage
{
    public GameListPage(GameListViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }
}