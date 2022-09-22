using Microsoft.Maui.Controls;

using OpenDictionary.DependencyInjection;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Views.Pages;
public partial class WordGroupDetailPage : ContentPage
{
    private readonly WordGroupDetailViewModel viewModel;

    public WordGroupDetailPage()
    {
        InitializeComponent();

        BindingContext = viewModel = DiContainer.Get<WordGroupDetailViewModel>();
    }
}