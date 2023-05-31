using System.Collections.Generic;
using System.Linq;

using OpenDictionary.Models;
using OpenDictionary.Observables.Metadatas;

namespace OpenDictionary.Observables;

public static class WordMetadataExtension
{
    public static WordMetadata? AsMetadata(this WordMetadataObservable? observable)
    {
        if (observable is null)
        {
            return null;
        }

        WordMetadata metadata = new WordMetadata
        {
            Value = observable.Word,
            Phonetics = observable.Phonetics.Select(static entity => new Phonetic
            {
                Value = entity.Pronunciation!,
                Audio = entity.Source,
            }).ToArray(),
            Meanings = observable.Meanings.Select(static entity => new Meaning
            {
                PartOfSpeech = entity.PartOfSpeech!,
                Synonyms = new Synonyms { Value = entity.Synonyms! },
                Antonyms = new Antonyms { Value = entity.Antonyms! },
                Definitions = entity.Definitions.Select(static entity => new Definition
                {
                    Value = entity.Value!,
                    Example = entity.Example
                }).ToArray()
            }).ToArray()
        };

        return metadata;
    }

    public static WordMetadataObservable? AsObservable(this WordMetadata? metadata)
    {
        if (metadata is null)
        {
            return null;
        }

        var observable = new WordMetadataObservable()
            .Copy(from: metadata);

        return observable;
    }

    public static WordMetadataObservable Copy(this WordMetadataObservable to, WordMetadata from)
    {
        to.Word = from.Value;

        to.AddPhonetics(from.Value, from.Phonetics).AddMeanings(from.Meanings);

        return to;
    }
    public static WordMetadataObservable AddPhonetics(this WordMetadataObservable observable, string word, IEnumerable<Phonetic> phonetics)
    {
        var observables = phonetics.Select(phonetic => new PhoneticObservable
        {
            Word = word,
            Pronunciation = phonetic.Value,
            Source = phonetic.Audio,
        }).ToArray();

        observable.Phonetics.AddRange(observables, System.Collections.Specialized.NotifyCollectionChangedAction.Add);

        return observable;
    }

    public static WordMetadataObservable AddMeanings(this WordMetadataObservable observable, IEnumerable<Meaning> meanings)
    {
        var observables = meanings.Select(static meaning =>
        {
            var observable = new MeaningObservable
            {
                PartOfSpeech = meaning.PartOfSpeech,
                Synonyms = meaning.Synonyms?.Value,
                Antonyms = meaning.Antonyms?.Value,
            };


            observable.AddDefinitions(meaning.Definitions);

            return observable;
        }).ToArray();

        observable.Meanings.AddRange(observables, System.Collections.Specialized.NotifyCollectionChangedAction.Add);

        return observable;
    }
    public static void AddDefinitions(this MeaningObservable meaning, IEnumerable<Definition> definitions)
    {
        var observables = definitions.Select(static definition => new DefinitionObservable
        {
            Value = definition.Value,
            Example = definition.Example,
        }).ToArray();

        meaning.Definitions.AddRange(observables, System.Collections.Specialized.NotifyCollectionChangedAction.Add);
    }
}