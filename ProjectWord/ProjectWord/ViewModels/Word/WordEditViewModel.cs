#nullable enable

using System;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace OpenDictionary.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class WordEditViewModel : WordViewModel
    {
        private readonly IStorage<Word> wordStorage;
        private readonly INavigationService navigation;

        public AsyncCommand SaveCommand { get; }
        public Command DiscardCommand { get; }

        public WordEditViewModel(IStorage<Word> wordStorage, INavigationService navigation) : base(wordStorage, navigation)
        {
            this.wordStorage = wordStorage;
            this.navigation = navigation;

            SaveCommand = new AsyncCommand(OnSave, ValidateSave);
            DiscardCommand = new Command(OnDiscard);

            Word.PropertyChanged += (_, __) =>
            {
                SaveCommand.ChangeCanExecute();
            };
        }

        private async Task OnSave()
        {
            Guid guid = Guid.Parse(Id);

            Word word = await wordStorage.Query().GetById(guid);

            word.Origin = Word.Origin;
            word.Translation = Word.Translation;

            await wordStorage.UpdateAsync(word);

            // This will pop the current page off the navigation stack
            await navigation.GoBackAsync();
        }
        private async void OnDiscard(object obj)
        {
            // This will pop the current page off the navigation stack
            await navigation.GoBackAsync();
        }

        private bool ValidateSave()
        {
            return string.IsNullOrWhiteSpace(Word.Translation) == false;
        }
    }
}