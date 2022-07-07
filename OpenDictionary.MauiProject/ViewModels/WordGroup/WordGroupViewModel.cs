using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Maui.Controls;

using MvvmHelpers;

using OpenDictionary.Models;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
[QueryProperty(nameof(Id), nameof(Id))]
public abstract partial class WordGroupViewModel
{
    private string? id;

    [ObservableProperty]
    private string? name;

    public ObservableRangeCollection<Word> Words { get; } = new();

    public string? Id
    {
        get => id;
        set
        {
            id = value;

            Load();
        }
    }

    protected abstract void Load();
}