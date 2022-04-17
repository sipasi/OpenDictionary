using System.Collections.Generic;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Views.Pages;

using Xamarin.Forms;

namespace OpenDictionary.ViewModels
{
    public class WordGroupListViewModel : CollectionViewModel<WordGroup>
    {
        private readonly IStorage<WordGroup> wordGroupStorage;
        private readonly INavigationService navigation;

        public Command RedirectToCreateCommand { get; }


        public WordGroupListViewModel(IStorage<WordGroup> wordGroupStorage, INavigationService navigation)
        {
            this.wordGroupStorage = wordGroupStorage;
            this.navigation = navigation;

            RedirectToCreateCommand = new Command(OnCreateItemReditected);
        }

        protected override async Task<IEnumerable<WordGroup>> Load()
        {
            var all = await wordGroupStorage
                .Query()
                .OrderByDateDescending()
                .IncludeAll()
                .ToArrayAsync();

            return all;
        }

        protected async void OnCreateItemReditected(object obj)
        {
            await navigation.GoToAsync<WordGroupCreatePage>();
        }

        protected override async Task OnItemSelected(WordGroup item)
        {
            if (item == null) return;

            await navigation.GoToAsync<WordGroupDetailPage>(parameter: nameof(WordGroup.Id), value: item.Id.ToString());
        }
    }
}
