using System.Collections.Generic;
using System.Linq;

using MvvmHelpers;

using OpenDictionary.Models;
using OpenDictionary.ViewModels;


namespace OpenDictionary.Observables.Metadatas
{
    public class WordMetadataObservable : ViewModel
    {
        private string value;

        public string Value { get => value; set => SetProperty(ref this.value, value); }

        public ObservableRangeCollection<PhoneticObservable> Phonetics { get; }
        public ObservableRangeCollection<MeaningObservable> Meanings { get; }

        public WordMetadataObservable()
        {
            value = string.Empty;

            Phonetics = new ObservableRangeCollection<PhoneticObservable>();
            Meanings = new ObservableRangeCollection<MeaningObservable>();
        }

        public void Set(WordMetadata metadata)
        {
            Value = metadata.Value;

            AddPhonetics(metadata.Phonetics);

            AddMeanings(metadata.Meanings);
        }

        public WordMetadata AsMetadata()
        {
            WordMetadata metadata = new WordMetadata
            {
                Value = value,
                Phonetics = Phonetics.Select(entity => new Phonetic
                {
                    Value = entity.Value,
                    Audio = entity.Audio,
                }).ToArray(),
                Meanings = Meanings.Select(entity => new Meaning
                {
                    PartOfSpeech = entity.PartOfSpeech,
                    Synonyms = new Synonyms { Value = entity.Synonyms },
                    Antonyms = new Antonyms { Value = entity.Antonyms },
                    Definitions = entity.Definitions.Select(entity => new Definition
                    {
                        Value = entity.Value,
                        Example = entity.Example
                    }).ToArray()
                }).ToArray()
            };

            return metadata;
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

        private void AddPhonetics(IEnumerable<Phonetic> phonetics)
        {
            var observables = phonetics.Select(phonetic => new PhoneticObservable
            {
                Value = phonetic.Value,
                Audio = phonetic.Audio,
            }).ToArray();

            Phonetics.AddRange(observables, System.Collections.Specialized.NotifyCollectionChangedAction.Add);
        }

        private void AddMeanings(IEnumerable<Meaning> meanings)
        {
            var observables = meanings.Select(meaning =>
            {
                var observable = new MeaningObservable
                {
                    PartOfSpeech = meaning.PartOfSpeech,
                    Synonyms = meaning.Synonyms.Value,
                    Antonyms = meaning.Antonyms.Value,
                };


                AddDefinitions(meaning.Definitions, observable);

                return observable;
            }).ToArray();

            Meanings.AddRange(observables, System.Collections.Specialized.NotifyCollectionChangedAction.Add);
        }
        private void AddDefinitions(IEnumerable<Definition> definitions, MeaningObservable meaning)
        {
            var observables = definitions.Select(definition => new DefinitionObservable
            {
                Value = definition.Value,
                Example = definition.Example,
            }).ToArray();

            meaning.Definitions.AddRange(observables, System.Collections.Specialized.NotifyCollectionChangedAction.Add);
        }
    }
}