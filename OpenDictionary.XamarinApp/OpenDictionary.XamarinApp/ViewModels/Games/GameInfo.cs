using System;

namespace OpenDictionary.ViewModels.Games
{
    public class GameInfo : ViewModel
    {
        private int wordCount;

        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
        public int CountToUnlock { get; set; }

        public bool IsUnlock => WordCount >= CountToUnlock;
        public bool IsLock => !IsUnlock;

        public Type Page { get; set; }
    }
}