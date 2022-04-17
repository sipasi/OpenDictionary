#nullable enable

using System;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Observables.Words;
using OpenDictionary.Services.Navigations;

using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace OpenDictionary.ViewModels
{
    public class WordViewModel : ViewModel
    {
        private string id;

        private readonly IStorage<Word> wordStorage;
        private readonly INavigationService navigation;

        public string Id
        {
            get => id;
            set
            {
                id = value;

                _ = Load();
            }
        }

        public bool IsNew => string.IsNullOrWhiteSpace(id);

        public WordObservable Word { get; }
        public AsyncCommand LoadWordDataCommand { get; }

        public WordViewModel(IStorage<Word> wordStorage, INavigationService navigation)
        {
            this.wordStorage = wordStorage;
            this.navigation = navigation;

            id = string.Empty;

            Word = new WordObservable();

            LoadWordDataCommand = new AsyncCommand(LoadWord);
        }

        protected virtual async ValueTask Load()
        {
            await LoadWord();
        }

        private async Task LoadWord()
        {
            try
            {
                if (IsNew)
                {
                    throw new NotSupportedException();
                }

                Guid guid = Guid.Parse(id);

                Word word = await wordStorage.Query().GetById(guid);

                Word.Origin = word!.Origin;
                Word.Translation = word!.Translation;
            }
            catch (Exception e)
            {
                await ErrorMessage(e);

                await navigation.GoBackAsync();
            }
        }

        protected static Task ErrorMessage(Exception exception)
        {
            string title = nameof(WordEditViewModel);
            string message = exception.Message;
            string cancel = "Close";

            return Shell.Current.DisplayAlert(title, message, cancel);
        }
    }
}