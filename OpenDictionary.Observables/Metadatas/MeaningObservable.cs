using CommunityToolkit.Mvvm.ComponentModel;

using MvvmHelpers;


namespace OpenDictionary.Observables.Metadatas;

[INotifyPropertyChanged]
public sealed partial class MeaningObservable
{
    [ObservableProperty]
    private string? partOfSpeech;

    [ObservableProperty]
    private string? synonyms;
    [ObservableProperty]
    private string? antonyms;

    public ObservableRangeCollection<DefinitionObservable> Definitions { get; } = new();
}