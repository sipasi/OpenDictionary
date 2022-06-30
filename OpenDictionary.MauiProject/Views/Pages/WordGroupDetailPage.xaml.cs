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

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();

    //    var (tapped, collection) = (viewModel.TappedItem, viewModel.Words);

    //    if (tapped is null || collection.Contains(tapped) is false)
    //    {
    //        return;
    //    }

    //    int? index = collection.IndexOf(tapped);

    //    if (index is null or < 0)
    //    {
    //        return;
    //    }

    //    collectionView.ScrollTo(index, position: ScrollToPosition.Start, animate: true);
    //}
}