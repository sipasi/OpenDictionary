#nullable enable

using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.ViewModels.Helpers;
using OpenDictionary.XamarinApp.Services.Messages.Toasts;

using Xamarin.CommunityToolkit.ObjectModel;

namespace OpenDictionary.ViewModels
{
    public class WordGroupDictionaryViewModel : ViewModel
    {
        private readonly IStorage<WordGroup> storage;
        private readonly IDialogMessageService dialog;
        private readonly IToastMessageService toast;

        public AsyncCommand AddPreinstalledlCommand { get; }
        public AsyncCommand DeleteAllCommand { get; }

        public WordGroupDictionaryViewModel(IStorage<WordGroup> storage, IDialogMessageService dialog, IToastMessageService toast)
        {
            this.storage = storage;
            this.dialog = dialog;
            this.toast = toast;

            DeleteAllCommand = new AsyncCommand(OnDelete);
            AddPreinstalledlCommand = new AsyncCommand(OnAdd);
        }

        private async Task OnAdd()
        {
            WordGroup[]? groups = await WordGroupJsonFile.LoadAsync();

            if (groups is null)
            {
                await toast.ShowError();

                return;
            }
             
            int count = 0;

            foreach (var group in groups)
            {
                var result = await storage
                    .Query()
                    .Select(entity => new { Name = entity.Name })
                    .FirstAsync(entity => entity.Name == group.Name);

                if (result == null)
                {
                    count++;

                    await storage.AddAsync(group);
                }
            }

            await toast.ShowSuccess(message: GetSuccessMessage(count));
        }
        private async Task OnDelete()
        {
            DialogResult result = await EntityDeleteDialog.Show(dialog);

            if (result is DialogResult.Ok)
            {
                var groups = await storage.Query().IncludeAll().ToArrayAsync();

                await toast.ShowAfter(() => storage.DeleteRangeAsync(groups), success: GetSuccessMessage(count: groups.Length));
            }
        }

        private string GetSuccessMessage(int count)
        {
            return $"Success: {count}";
        }
    }
}