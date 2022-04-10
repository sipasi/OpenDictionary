
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Framework.Words;
using Framework.Words.DictionarySources;
using Framework.Words.Parsers;

using ProjectWord.Collections.Storages;
using ProjectWord.Collections.Storages.Extensions;
using ProjectWord.Models;

using Xamarin.Forms;


namespace ProjectWord.ViewModels
{
    public class WordMetadataObservable : ViewModel
    {
        private string value;
        private ObservableCollection<PhoneticObservable> phonetics;
        private ObservableCollection<MeaningObservable> meanings;

        public string Value { get => value; set => SetProperty(ref this.value, value); }

        public ObservableCollection<PhoneticObservable> Phonetics { get => phonetics; set => SetProperty(ref phonetics, value); }
        public ObservableCollection<MeaningObservable> Meanings { get => meanings; set => SetProperty(ref meanings, value); }

        public WordMetadataObservable()
        {
            Phonetics = new ObservableCollection<PhoneticObservable>();
            Meanings = new ObservableCollection<MeaningObservable>();
        }

        public void Set(IWordMetadata metadata)
        {
            Value = metadata.Value;

            foreach (var phonetic in metadata.Phonetics)
            {
                PhoneticObservable observable = new PhoneticObservable();

                Phonetics.Add(observable);

                observable.Value = phonetic.Value;
                observable.Audio = phonetic.Audio;
            }
            foreach (var meaning in metadata.Meanings)
            {
                MeaningObservable observable = new MeaningObservable();

                Meanings.Add(observable);

                observable.PartOfSpeech = meaning.PartOfSpeech;

                foreach (var definition in meaning.Definitions)
                {
                    DefinitionObservable definitionObservable = new DefinitionObservable();

                    definitionObservable.Value = definition.Value;
                    definitionObservable.Example = definition.Example;

                    observable.Definitions.Add(definitionObservable);
                }
            }
        }
    }
    public class PhoneticObservable : ViewModel
    {
        private string value;
        private string audio;

        public string Value { get => value; set => SetProperty(ref this.value, value); }
        public string Audio { get => audio; set => SetProperty(ref this.audio, value); }
    }
    public class MeaningObservable : ViewModel
    {
        private string partOfSpeech;
        private ObservableCollection<DefinitionObservable> definitions;
         
        public string PartOfSpeech { get => partOfSpeech; set => SetProperty(ref partOfSpeech, value); }
        public ObservableCollection<DefinitionObservable> Definitions { get => definitions; set => SetProperty(ref definitions, value); }

        public MeaningObservable()
        {
            Definitions = new ObservableCollection<DefinitionObservable>();
        }
    }
    public class DefinitionObservable : ViewModel
    {
        private string value;
        private string example;
        private string[] synonyms;
        private string[] antonyms;

        public string Value { get => value; set => SetProperty(ref this.value, value); }
        public string Example { get => example; set => SetProperty(ref example, value); }
        public string[] Synonyms { get => synonyms; set => SetProperty(ref synonyms, value); }
        public string[] Antonyms { get => antonyms; set => SetProperty(ref antonyms, value); }
    }


    [QueryProperty(nameof(Id), nameof(Id))]
    public class WordEditViewModel : StorageViewModel
    {
        private string id;
        private string origin;
        private string translation;

        private WordMetadataObservable metadata;

        public string Id
        {
            get => id;
            set
            {
                id = value;

                LoadItems();
            }
        }

        public bool IsNew => string.IsNullOrWhiteSpace(id);

        public string Origin { get => origin; set => SetProperty(ref origin, value); }
        public string Translation { get => translation; set => SetProperty(ref translation, value); }

        public WordMetadataObservable Metadata { get => metadata; set => SetProperty(ref metadata, value); }

        public Command SaveCommand { get; }
        public Command DiscardCommand { get; }
        public Command LoadMetaDataCommand { get; }

        public WordEditViewModel()
        {
            SaveCommand = new Command(OnSave);
            DiscardCommand = new Command(OnDiscard);

            Metadata = new WordMetadataObservable();
        }

        private async void OnSave(object obj)
        {
            IStorage<Word> storage = GetStorage<Word>();

            Guid guid = Guid.Parse(id);

            Word word = await storage.Query().GetById(guid);

            word.Origin = origin;
            word.Translation = translation;

            await storage.UpdateAsync(word);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnDiscard(object obj)
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        protected override async Task Load()
        {
            if (IsNew)
            {
                throw new NotSupportedException();
            }

            try
            {
                IStorage<Word> storage = GetStorage<Word>();

                Guid guid = Guid.Parse(id);

                Word word = await storage.Query().GetById(guid);

                Origin = word.Origin;
                Translation = word.Translation;

                DictionaryApiJsonParser parser = new DictionaryApiJsonParser();
                DictionaryApiRemoteSource source = new DictionaryApiRemoteSource(parser);

                var metadata = await source.GetWord(word.Origin);

                Metadata.Set(metadata);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Failed to Load Item. {e.Message}");
            }
        }
    }
}
