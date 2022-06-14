
using System;
using System.Linq;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;

using Xamarin.Forms;

namespace OpenDictionary.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class WordGroupViewModel : ViewModel
    {
        private string id;
        private string name;

        private readonly IStorage<WordGroup> wordGroupStorage;
        private readonly INavigationService navigation;

        public Word TappedWord { get; private set; }

        public CollectionViewModel<Word> Words { get; }

        public string Id
        {
            get => id;
            set
            {
                id = value;

                Words.LoadCommand.ExecuteAsync();
            }
        }
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public WordGroupViewModel(IStorage<WordGroup> wordGroupStorage, INavigationService navigation)
        {
            this.wordGroupStorage = wordGroupStorage;
            this.navigation = navigation;

            Words = new CollectionViewModel<Word>(Load, OnTapped);
        }

        private Task OnTapped(Word item)
        {
            TappedWord = item;

            return navigation.GoToAsync(AppRoutes.Word.Detail, parameter: nameof(Word.Id), value: item.Id.ToString());
        }

        public async Task Load()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return;
            }

            Guid guid = Guid.Parse(id);

            WordGroup group = await wordGroupStorage
                .Query()
                .IncludeAll()
                .GetById(guid);

            Name = group.Name;

            var items = group.Words.OrderByDescending(word => word.Date);

            Words.Collection.Clear();

            Words.Collection.AddRange(items);
        }
    }
}