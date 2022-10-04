using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages;

public partial class WordGroupListPage : ContentPage
{
    private readonly WordGroupInfoList viewModel;

    public WordGroupListPage()
    {
        InitializeComponent();

        BindingContext = viewModel = DiContainer.Get<WordGroupInfoList>();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await viewModel.Groups.LoadCommand!.ExecuteAsync(default);
    }
}