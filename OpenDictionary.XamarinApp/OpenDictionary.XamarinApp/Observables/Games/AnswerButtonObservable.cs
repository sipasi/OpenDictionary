using OpenDictionary.ViewModels;


namespace OpenDictionary.Observables.Games
{
    public class AnswerButtonObservable : ViewModel
    {
        private string text;

        public string Text { get => text; set => SetProperty(ref this.text, value); }
        public bool IsCorrect { get; set; }
    }
}
