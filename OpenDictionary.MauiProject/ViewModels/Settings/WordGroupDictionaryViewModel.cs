#nullable enable

using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.ViewModels.Helpers;
using OpenDictionary.XamarinApp.Services.Messages.Toasts;

namespace OpenDictionary.ViewModels;

public sealed partial class WordGroupDictionaryViewModel
{
    private readonly IStorage<WordGroup> storage;
    private readonly IDialogMessageService dialog;
    private readonly IToastMessageService toast;

    public WordGroupDictionaryViewModel(IStorage<WordGroup> storage, IDialogMessageService dialog, IToastMessageService toast)
    {
        this.storage = storage;
        this.dialog = dialog;
        this.toast = toast;
    }

    [RelayCommand]
    private async Task AddPreinstalled()
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
                .FirstOrDefaultAsync(entity => entity.Name == group.Name);

            if (result is null)
            {
                count++;

                await storage.AddAsync(group);
            }
        }

        await toast.ShowSuccess(message: GetSuccessMessage(count));
    }
    [RelayCommand]
    private async Task DeleteAll()
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