using OpenDictionary.ViewModels;

namespace OpenDictionary.Games.WordConformities.Observables
{
    public class AnswerButtonObservable : ViewModel
    {
        private string text = string.Empty;

        public string Text { get => text; set => SetProperty(ref text, value); }
        public bool IsCorrect { get; set; }
    }
}