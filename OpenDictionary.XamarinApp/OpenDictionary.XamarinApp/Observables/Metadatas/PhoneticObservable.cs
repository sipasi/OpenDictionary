using OpenDictionary.ViewModels;


namespace OpenDictionary.Observables.Metadatas
{
    public class PhoneticObservable : ViewModel
    {
        private string value;
        private string audio;

        public string Value { get => value; set => SetProperty(ref this.value, value); }
        public string Audio { get => audio; set => SetProperty(ref this.audio, value); }
    }
}