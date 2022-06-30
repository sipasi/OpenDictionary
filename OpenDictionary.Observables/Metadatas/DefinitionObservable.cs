using OpenDictionary.ViewModels;


namespace OpenDictionary.Observables.Metadatas
{
    public class DefinitionObservable : ViewModel
    {
        private string? value = string.Empty;
        private string? example = string.Empty;

        public string? Value { get => value; set => SetProperty(ref this.value, value); }
        public string? Example { get => example; set => SetProperty(ref example, value); }
    }
}