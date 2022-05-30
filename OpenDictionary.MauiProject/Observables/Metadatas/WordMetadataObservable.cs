using System.Collections.Generic;
using System.Linq;

using Framework.Words;

using MvvmHelpers;

using OpenDictionary.ViewModels;

namespace OpenDictionary.Observables.Metadatas;

public class WordMetadataObservable : ViewModel
{
    private string value;

    public string Value { get => value; set => SetProperty(ref this.value, value); }

    public ObservableRangeCollection<PhoneticObservable> Phonetics { get; }
    public ObservableRangeCollection<MeaningObservable> Meanings { get; }

    public WordMetadataObservable()
    {
        Phonetics = new ObservableRangeCollection<PhoneticObservable>();
        Meanings = new ObservableRangeCollection<MeaningObservable>();
    }

    public void Set(IWordMetadata metadata)
    {
        Value = metadata.Value;

        AddPhonetics(metadata.Phonetics);

        AddMeanings(metadata.Meanings);
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

    private void AddPhonetics(IEnumerable<IPhonetic> phonetics)
    {
        var observables = phonetics.Select(phonetic => new PhoneticObservable
        {
            Value = phonetic.Value,
            Audio = phonetic.Audio,
        });

        Phonetics.AddRange(observables);
    }

    private void AddMeanings(IEnumerable<IMeaning> meanings)
    {
        var observables = meanings.Select(meaning =>
        {
            var observable = new MeaningObservable
            {
                PartOfSpeech = meaning.PartOfSpeech,
                Synonyms = string.Join(", ", meaning.Synonyms),
                Antonyms = string.Join(", ", meaning.Antonyms),
            };


            AddDefinitions(meaning.Definitions, observable);

            return observable;
        });

        Meanings.AddRange(observables);
    }
    private void AddDefinitions(IEnumerable<IWordDefinition> definitions, MeaningObservable meaning)
    {
        var observables = definitions.Select(definition => new DefinitionObservable
        {
            Value = definition.Value,
            Example = definition.Example,
        });

        meaning.Definitions.AddRange(observables);
    }
}