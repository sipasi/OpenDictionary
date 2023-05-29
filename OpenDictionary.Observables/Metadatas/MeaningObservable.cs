using CommunityToolkit.Mvvm.ComponentModel;

using MvvmHelpers;

namespace OpenDictionary.Observables.Metadatas;

public sealed partial class MeaningObservable : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    [ObservableProperty]
    private string? partOfSpeech;

    [ObservableProperty]
    private string? synonyms;

    [ObservableProperty]
    private string? antonyms;

    public ObservableRangeCollection<DefinitionObservable> Definitions { get; } = new();
}