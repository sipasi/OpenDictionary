using System;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MvvmHelpers;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
public partial class CollectionViewModel<T>
{
    [ObservableProperty]
    private bool isBusy;

    public ObservableRangeCollection<T> Collection { get; }

    public AsyncRelayCommand? LoadCommand { get; init; }

    public AsyncRelayCommand<T>? TappedCommand { get; init; }

    public CollectionViewModel()
    {
        Collection = new();
    }
    public CollectionViewModel(Func<Task> loadCommand, Func<T?, Task> tappedCommand) : this()
    {
        LoadCommand = new(loadCommand);
        TappedCommand = new(tappedCommand);
    }
}