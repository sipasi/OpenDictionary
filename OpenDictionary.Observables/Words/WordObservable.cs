using OpenDictionary.ViewModels;

namespace OpenDictionary.Observables.Words
{
    public class WordObservable : ViewModel
    {
        private string origin = string.Empty;
        private string translation = string.Empty;

        public string Origin { get => origin; set => SetProperty(ref this.origin, value); }
        public string Translation { get => translation; set => SetProperty(ref translation, value); }
    }
}