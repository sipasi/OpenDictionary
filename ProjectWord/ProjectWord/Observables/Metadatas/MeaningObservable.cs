
using OpenDictionary.ViewModels;

using Xamarin.CommunityToolkit.ObjectModel;

namespace OpenDictionary.Observables.Metadatas
{
    public class MeaningObservable : ViewModel
    {
        private string partOfSpeech;

        public string PartOfSpeech { get => partOfSpeech; set => SetProperty(ref partOfSpeech, value); }
        public ObservableRangeCollection<DefinitionObservable> Definitions { get; }

        public MeaningObservable()
        {
            Definitions = new ObservableRangeCollection<DefinitionObservable>();
        }
    }
}