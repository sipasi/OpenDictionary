using OpenDictionary.ViewModels;


namespace OpenDictionary.Observables.Metadatas
{
    public class PhoneticObservable : ViewModel
    {
        private string? value = string.Empty;
        private string? audio = string.Empty;

        public string? Value { get => value; set => SetProperty(ref this.value, value); }
        public string? Audio { get => audio; set => SetProperty(ref this.audio, value); }
    }
}