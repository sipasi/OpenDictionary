#nullable enable

using System;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MvvmHelpers;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
public sealed partial class SelectableCollectionViewModel<T> where T : class
{
    [ObservableProperty]
    private ISelectionState state;

    private readonly Action? selectionsChanged;

    public ObservableRangeCollection<T> Items { get; }
    public ObservableRangeCollection<object> SelectedItems { get; }

    public Action? SelectionsChanged { init => selectionsChanged = value; }

    public SelectableCollectionViewModel()
    {
        state = SelectionStates.None;

        Items = new();
        SelectedItems = new();

        SelectedItems.CollectionChanged += SelectedItems_CollectionChanged;
    }

    private void SelectedItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        selectionsChanged?.Invoke();

        const int empty = 0;
        int selected = SelectedItems.Count;
        int all = Items.Count;

        State = selected is empty
            ? SelectionStates.None
            : selected != all
            ? SelectionStates.Indeterminate
            : SelectionStates.All;
    }

    [RelayCommand]
    private void SelectAll()
    {
        int all = Items.Count;
        int selected = SelectedItems.Count;

        SelectedItems.Clear();

        if (selected < all)
        {
            SelectedItems.AddRange(Items);
        }
    }
}
