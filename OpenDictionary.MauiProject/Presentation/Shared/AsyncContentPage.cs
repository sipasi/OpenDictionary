#nullable enable

using Microsoft.Maui.Controls;

namespace OpenDictionary.Presentation.Shared;

public class AsyncContentPage : ContentPage
{
    private readonly AsyncObservableObject viewModel;

    public AsyncContentPage(AsyncObservableObject viewModel)
    {
        BindingContext = viewModel;

        this.viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.Initialize();
    }
}