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
            });

            Phonetics.AddRange(observables);
        }

        private void AddMeanings(IEnumerable<Meaning> meanings)
        {
            var observables = meanings.Select(meaning =>
            {
                var observable = new MeaningObservable
                {
                    PartOfSpeech = meaning.PartOfSpeech,
                    Synonyms = string.Join(", ", meaning.Synonyms.Select(entity => entity.Value)),
                    Antonyms = string.Join(", ", meaning.Antonyms.Select(entity => entity.Value)),
                };


                AddDefinitions(meaning.Definitions, observable);

                return observable;
            });

            Meanings.AddRange(observables);
        }
        private void AddDefinitions(IEnumerable<Definition> definitions, MeaningObservable meaning)
        {
            var observables = definitions.Select(definition => new DefinitionObservable
            {
                Value = definition.Value,
                Example = definition.Example,
            });

            meaning.Definitions.AddRange(observables);
        }
    }
}