using ProjectWord.ViewModels;


namespace ProjectWord.Observables.Words
{
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
}