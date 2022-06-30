using MvvmHelpers;

using OpenDictionary.ViewModels;

namespace OpenDictionary.Observables.Metadatas
{
    public class MeaningObservable : ViewModel
    {
        private string? partOfSpeech;

        private string? synonyms;
        private string? antonyms;

        public string? PartOfSpeech { get => partOfSpeech; set => SetProperty(ref partOfSpeech, value); }

        public ObservableRangeCollection<DefinitionObservable> Definitions { get; }

        public string? Synonyms { get => synonyms; set => SetProperty(ref synonyms, value); }
        public string? Antonyms { get => antonyms; set => SetProperty(ref antonyms, value); }

        public MeaningObservable()
        {
            Definitions = new ObservableRangeCollection<DefinitionObservable>();
        }
    }
}