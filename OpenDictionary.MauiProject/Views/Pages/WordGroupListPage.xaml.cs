using Microsoft.Maui.Controls;

using OpenDictionary.ViewModels.WordGroups;

namespace OpenDictionary.Views.Pages;

public partial class WordGroupListPage : ContentPage
{
    private readonly WordGroupInfoList viewModel;

    public WordGroupListPage(WordGroupInfoList viewModel)
    {
        InitializeComponent();

        BindingContext = this.viewModel = viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.Groups.LoadCommand!.ExecuteAsync(default);
    }
}