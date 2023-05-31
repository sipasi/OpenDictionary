using CommunityToolkit.Mvvm.ComponentModel;

using MvvmHelpers;

namespace OpenDictionary.Observables.Metadatas;

public sealed partial class WordMetadataObservable : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    [ObservableProperty]
    private string word;

    public ObservableRangeCollection<PhoneticObservable> Phonetics { get; }
    public ObservableRangeCollection<MeaningObservable> Meanings { get; }

    public WordMetadataObservable()
    {
        word = string.Empty;

        Phonetics = new();
        Meanings = new();
    }

    public void Clear()
    {
        Phonetics.Clear();

        foreach (var meaning in Meanings)
        {
            meaning.Definitions.Clear();
        }

        Meanings.Clear();
    }
}