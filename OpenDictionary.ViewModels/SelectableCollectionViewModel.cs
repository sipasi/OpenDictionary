#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Specialized;

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

    private readonly ObservableRangeCollection<T> items;
    private readonly ObservableRangeCollection<object> selected;

    public IReadOnlyList<T> Items => items;
    public IReadOnlyList<object> SelectedItems => selected;

    public Action? SelectionsChanged { init => selectionsChanged = value; }

    public SelectableCollectionViewModel()
    {
        state = SelectionStates.None;

        items = new();
        selected = new();

        selected.CollectionChanged += SelectedItems_CollectionChanged;
    }

    public void Clear()
    {
        items.Clear();
        selected.Clear();
    }
    public void ClearSelected() => selected.Clear();

    public void Add(T item) => items.Add(item);
    public void AddRange(IEnumerable<T> items) => this.items.AddRange(items);

    private void SelectedItems_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        selectionsChanged?.Invoke();

        const int empty = 0;
        int selected = this.selected.Count;
        int all = items.Count;

        State = selected is empty
            ? SelectionStates.None
            : selected != all
            ? SelectionStates.Indeterminate
            : SelectionStates.All;
    }

    [RelayCommand]
    private void SelectAll()
    {
        int all = items.Count;
        int selected = this.selected.Count;

        this.selected.Clear();

        if (selected < all)
        {
            this.selected.AddRange(items);
        }
    }
}