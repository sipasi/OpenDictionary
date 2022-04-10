
using System;
using System.Collections.Generic;
using System.Linq;

using ProjectWord.Collections.Storages;
using ProjectWord.Collections.Storages.Extensions;
using ProjectWord.Models;

using Xamarin.Forms;

namespace ProjectWord.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class WordGroupEditViewModel : WordGroupBaseViewModel
    {
        private string origin;
        private string translation;

        private readonly List<Word> created;
        private readonly List<Word> deleted;

        public string Origin
        {
            get => origin;
            set => SetProperty(ref origin, value);
        }
        public string Translation
        {
            get => translation;
            set => SetProperty(ref translation, value);
        }

        public bool IsNew => string.IsNullOrWhiteSpace(Id);

        public Command AddWordCommand { get; }
        public Command<Word> DeleteWordCommand { get; }

        public Command SaveCommand { get; }
        public Command DiscardCommand { get; }

        public WordGroupEditViewModel()
        {
            created = new List<Word>();
            deleted = new List<Word>();

            AddWordCommand = new Command(OnAddWord, ValidateAdd);
            SaveCommand = new Command(OnSave, ValidateSave);
            DiscardCommand = new Command(OnDiscard);

            DeleteWordCommand = new Command<Word>(OnDeleteWord);

            PropertyChanged += (_, __) =>
            {
                AddWordCommand.ChangeCanExecute();
                SaveCommand.ChangeCanExecute();
            };
        }

        private bool ValidateAdd()
        {
            return string.IsNullOrWhiteSpace(origin) == false &&
                   string.IsNullOrWhiteSpace(translation) == false;
        }
        private bool ValidateSave()
        {
            return string.IsNullOrWhiteSpace(Name) == false;
        }

        private void OnAddWord()
        {
            Word word = new Word
            {
                Date = DateTime.Now,
                Origin = origin,
                Translation = translation
            };

            Origin = null;
            Translation = null;

            Items.Add(word);
            created.Add(word);
        }
        private void OnDeleteWord(Word word)
        {
            if (word is null)
            {
                return;
            }

            Items.Remove(word);

            if (created.Contains(word) is false)
            {
                deleted.Add(word);
            }
        }

        private async void OnSave()
        {
            IStorage<Word> wordStorage = GetStorage<Word>();
            IStorage<WordGroup> groupStorage = GetStorage<WordGroup>();

            if (IsNew)
            {
                WordGroup group = new WordGroup()
                {
                    Name = Name,
                    Words = Items?.ToList()
                };

                await groupStorage.AddAsync(group);
            }
            else
            {
                var id = Guid.Parse(Id);

                WordGroup group = await groupStorage.Query().GetById(id);

                group.Words = Items?.ToList();

                foreach (var word in deleted)
                {
                    await wordStorage.DeleteAsync(word);
                }

                await groupStorage.UpdateAsync(group);
            }


            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        private async void OnDiscard()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
