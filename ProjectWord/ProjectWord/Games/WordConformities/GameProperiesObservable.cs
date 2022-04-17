using OpenDictionary.ViewModels;

namespace OpenDictionary.Games.WordConformities
{
    public class GamePropertiesObservable : ViewModel, IProperties
    {
        private string groupName;

        private string question;

        private int total;

        private int answered;

        private int correct;
        private int uncorrect;

        public string GroupName { get => groupName; set => SetProperty(ref groupName, value); }
        public string Question { get => question; set => SetProperty(ref question, value); }

        public int Total { get => total; set => SetProperty(ref total, value); }

        public int Answered { get => answered; set => SetProperty(ref answered, value); }
        public int Correct { get => correct; set => SetProperty(ref correct, value); }
        public int Uncorrect { get => uncorrect; set => SetProperty(ref uncorrect, value); }

        public void Clear()
        {
            GroupName = Question = null;

            Total = Answered = Correct = Uncorrect = 0;
        }
    }
}
