using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using ProjectWord.Collections.Storages.Extensions;
using ProjectWord.Models;
using ProjectWord.Views.Pages;

using Xamarin.Forms;

namespace ProjectWord.ViewModels
{
    public class WordGroupListViewModel : CollectionViewModel<WordGroup>
    {
        public Command RedirectToCreateCommand { get; }

        public WordGroupListViewModel()
        {
            RedirectToCreateCommand = new Command(OnCreateItemReditected);
        }

        protected override async Task Load()
        {
            var storage = GetStorage<WordGroup>();

            var all = await storage.Query().OrderByDateDescending().Include(entity => entity.Words).ToArrayAsync();

            Items.Clear();

            foreach (var item in all)
            {
                Items.Add(item);
            }
        }

        protected async void OnCreateItemReditected(object obj)
        {
            await Shell.Current.GoToAsync(nameof(WordGroupCreatePage));
        }

        protected override async void OnItemSelected(WordGroup item)
        {
            if (item == null) return;

            await Shell.Current.GoToAsync($"{nameof(WordGroupDetailPage)}?{nameof(WordGroup.Id)}={item.Id}");
        }
    }
}
