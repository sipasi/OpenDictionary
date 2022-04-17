
using System;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Views.Pages;

using Xamarin.Forms;

namespace OpenDictionary.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class WordGroupDetailViewModel : WordGroupBaseViewModel
    {
        private readonly IStorage<WordGroup> wordGroupStorage;
        private readonly INavigationService navigation;

        public Command RedirectToEditCommand { get; }
        public Command DeleteCommand { get; }

        public WordGroupDetailViewModel(IStorage<WordGroup> wordGroupStorage, INavigationService navigation) : base(wordGroupStorage, navigation)
        {
            this.wordGroupStorage = wordGroupStorage;
            this.navigation = navigation;

            RedirectToEditCommand = new Command(OnRedirectToEdit);
            DeleteCommand = new Command(OnDelete);
        }

        private async void OnRedirectToEdit(object obj)
        {
            await navigation.GoToAsync<WordGroupCreatePage>(parameter: nameof(WordGroup.Id), value: Id);
        }
        private async void OnDelete(object obj)
        {
            IStorage<WordGroup> storage = wordGroupStorage;

            Guid id = Guid.Parse(Id);

            WordGroup group = await storage.Query().IncludeAll().GetById(id);

            await storage.DeleteAsync(group);

            await navigation.GoBackAsync();
        }
    }
}
