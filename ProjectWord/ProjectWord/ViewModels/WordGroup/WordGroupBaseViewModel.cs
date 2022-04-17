
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Views.Pages;


namespace OpenDictionary.ViewModels
{
    public abstract class WordGroupBaseViewModel : CollectionViewModel<Word>
    {
        private string id;
        private string name;

        private readonly IStorage<WordGroup> wordGroupStorage;
        private readonly INavigationService navigation;

        public WordGroupBaseViewModel(IStorage<WordGroup> wordGroupStorage, INavigationService navigation)
        {
            this.wordGroupStorage = wordGroupStorage;
            this.navigation = navigation;
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;

                LoadItemsCommand.ExecuteAsync();
            }
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        protected override async Task OnItemSelected(Word item)
        {
            await navigation.GoToAsync<WordDetailPage>(parameter: nameof(Word.Id), value: item.Id.ToString());
        }

        protected override Task<IEnumerable<Word>> Load()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Task.FromResult(Array.Empty<Word>() as IEnumerable<Word>);
            }

            Guid guid = Guid.Parse(id);

            return Load(guid);
        }

        private async Task<IEnumerable<Word>> Load(Guid id)
        {
            try
            {
                WordGroup item = await wordGroupStorage.Query().IncludeAll().GetById(id);

                Name = item.Name;

                return item.Words.OrderByDescending(word => word.Date);
            }
            catch (Exception)
            {
                return Array.Empty<Word>();
            }
        }
    }
}
