using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.Maui.Controls;

using OpenDictionary.Models;

namespace OpenDictionary.WordGroups.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public abstract partial class WordGroupViewModel : ObservableObject
{
    private string? id;

    [ObservableProperty]
    private string? name;

    public MvvmHelpers.ObservableRangeCollection<Word> Words { get; } = new();

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