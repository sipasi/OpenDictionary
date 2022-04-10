
using System;

using Microsoft.EntityFrameworkCore;

using ProjectWord.Collections.Storages;
using ProjectWord.Collections.Storages.Extensions;
using ProjectWord.Models;
using ProjectWord.Views.Pages;

using Xamarin.Forms;

namespace ProjectWord.ViewModels
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public class WordGroupDetailViewModel : WordGroupBaseViewModel
    {
        public Command RedirectToEditCommand { get; }
        public Command DeleteCommand { get; }

        public WordGroupDetailViewModel()
        {
            RedirectToEditCommand = new Command(OnRedirectToEdit);
            DeleteCommand = new Command(OnDelete);
        }

        private async void OnRedirectToEdit(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(WordGroupCreatePage)}?{nameof(WordGroup.Id)}={Id}");
        }
        private async void OnDelete(object obj)
        {
            IStorage<WordGroup> storage = GetStorage<WordGroup>();

            Guid id = Guid.Parse(Id);

            WordGroup group = await storage.Query().Include(entity => entity.Words).GetById(id);

            await storage.DeleteAsync(group);

            await Shell.Current.GoToAsync("..");
        }
    }
}
