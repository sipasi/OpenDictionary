
using System;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;
using OpenDictionary.ViewModels.Helpers;
using OpenDictionary.Views.Pages;

using Xamarin.CommunityToolkit.ObjectModel;

namespace OpenDictionary.ViewModels
{
    public sealed class WordGroupDetailViewModel : WordGroupViewModel
    {
        private readonly IStorage<WordGroup> wordGroupStorage;
        private readonly INavigationService navigation;
        private readonly IDialogMessageService dialog;

        public AsyncCommand RedirectToGameCommand { get; }
        public AsyncCommand RedirectToEditCommand { get; }
        public AsyncCommand DeleteCommand { get; }

        public WordGroupDetailViewModel(IStorage<WordGroup> wordGroupStorage, INavigationService navigation, IDialogMessageService dialog) : base(wordGroupStorage, navigation)
        {
            this.wordGroupStorage = wordGroupStorage;
            this.navigation = navigation;
            this.dialog = dialog;

            RedirectToGameCommand = new AsyncCommand(OnRedirectToGame);
            RedirectToEditCommand = new AsyncCommand(OnRedirectToEdit);
            DeleteCommand = new AsyncCommand(OnDelete);
        }

        private async Task OnRedirectToGame()
        {
            await navigation.GoToAsync(AppRoutes.Game.List, parameter: nameof(WordGroup.Id), value: Id);
        }
        private async Task OnRedirectToEdit()
        {
            await navigation.GoToAsync(AppRoutes.WordGroup.Create, parameter: nameof(WordGroup.Id), value: Id);
        }
        private async Task OnDelete()
        {
            IStorage<WordGroup> storage = wordGroupStorage;

            Guid id = Guid.Parse(Id);

            DialogResult result = await EntityDeleteDialog.Show(dialog);

            if (result is DialogResult.Ok)
            {
                WordGroup group = await storage.Query().IncludeAll().GetById(id);

                await storage.DeleteAsync(group);

                await navigation.GoBackAsync();
            }
        }
    }
}