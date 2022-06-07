using OpenDictionary.ViewModels;

namespace OpenDictionary.Games.WordConformities.Observables
{
    public class GameInfo : ViewModel
    {
        private string image;
        private string name;
        private string description;
        private int wordCount;
        private int countToUnlock;
        private string route;

        public string Image { get => image; set => SetProperty(ref image, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Description { get => description; set => SetProperty(ref description, value); }
        public int WordCount
        {
            get => wordCount;
            set
            {
                SetProperty(ref wordCount, value);

                OnPropertyChanged(nameof(IsUnlock));
                OnPropertyChanged(nameof(IsLock));
            }
        }
        public int CountToUnlock { get => countToUnlock; set => SetProperty(ref countToUnlock, value); }

        public bool IsUnlock => WordCount >= CountToUnlock;
        public bool IsLock => !IsUnlock;

        public string Route { get => route; set => SetProperty(ref route, value); }
    }
}