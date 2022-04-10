
using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProjectWord.Collections.Storages;
using ProjectWord.Collections.Storages.Extensions;
using ProjectWord.Models;
using ProjectWord.Views.Pages;

using Xamarin.Forms;

namespace ProjectWord.ViewModels
{
    public abstract class WordGroupBaseViewModel : CollectionViewModel<Word>
    {
        private string id;
        private string name;

        public string Id
        {
            get => id;
            set
            {
                id = value;

                Task.Run(Load);
            }
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        protected override async void OnItemSelected(Word item)
        {
            await Shell.Current.GoToAsync($"{nameof(WordEditPage)}?{nameof(Word.Id)}={item.Id}");
        }

        protected override Task Load()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Task.CompletedTask;
            }

            Guid guid = Guid.Parse(id);

            return Load(guid);
        }

        private async Task Load(Guid id)
        {
            try
            {
                IStorage<WordGroup> storage = GetStorage<WordGroup>();

                WordGroup item = await storage.Query().Include(entity => entity.Words).GetById(id);

                Name = item.Name;

                Items.Clear();

                foreach (Word word in item.Words)
                {
                    Items.Add(word);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Failed to Load Item. {e.Message}");
            }
        }
    }
}
