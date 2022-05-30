using System;

using Microsoft.Maui.Controls;

using OpenDictionary.Collections.Extensions;
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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var tapped = viewModel.TappedWord;

        if (tapped is null) //  || viewModel.Words.Collection.Contains(lastSelected) is false
        {
            return;
        }

        var collection = viewModel.Words.Collection;

        int? index = collection.IndexOf(word => word.Origin == tapped.Origin);

        if (index is null or < 0)
        {
            return;
        }

        collectionView.ScrollTo(index, position: ScrollToPosition.Start, animate: false);
    }
}