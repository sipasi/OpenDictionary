#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Storage;

using Newtonsoft.Json;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Loadings;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.ViewModels.Helpers;
using OpenDictionary.XamarinApp.Services.Messages.Toasts;

namespace OpenDictionary.ViewModels;

public sealed partial class WordGroupDictionaryViewModel
{
    private readonly IStorage<WordGroup> storage;
    private readonly ILoadingMessageService loading;
    private readonly IDialogMessageService dialog;
    private readonly IToastMessageService toast;

    public IAsyncRelayCommand AddPreinstalledCommand { get; }

    public WordGroupDictionaryViewModel(IStorage<WordGroup> storage, IDialogMessageService dialog, IToastMessageService toast, ILoadingMessageService loading)
    {
        this.storage = storage;
        this.loading = loading;
        this.dialog = dialog;
        this.toast = toast;

        AddPreinstalledCommand = new AsyncRelayCommand(() => loading.Show("Adding", string.Empty, AddPreinstalled));
    }

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

        string message = count > 0 ? $"{count} dictionaries are installed" : "All dictionaries have already preinstalled";

        await toast.ShowSuccess(message);
    }
    [RelayCommand]
    private async Task DeleteAll()
    {
        DialogResult result = await EntityDeleteDialog.Show(dialog);

        if (result is DialogResult.Ok)
        {
            int count = 0;

            await loading.Show("Deleting", string.Empty, async () =>
            {
                var groups = await storage.Query().IncludeAll().ToArrayAsync();

                if (groups.Length is 0)
                {
                    return;
                }

                await Task.Delay(500);

                count = groups.Length;

                await storage.DeleteRangeAsync(groups);
            });

            string message = count > 0 ? "All dictionaries have been removed" : "Have no dictionaries to remove";

            await toast.Show(message);
        }
    }
}

file static class WordGroupJsonFile
{
    public static async Task<WordGroup[]?> LoadAsync()
    {
        using StreamReader reader = await GetReader();

        string json = await reader.ReadToEndAsync();

        return DeserializeAndFillDates(json);
    }

    public static WordGroup[]? DeserializeAndFillDates(string json)
    {
        var result = JsonConvert.DeserializeObject<WordGroup[]>(json);

        FillDates(result);

        return result;
    }

    private static void FillDates(WordGroup[]? groups)
    {
        if (groups is null)
        {
            return;
        }

        DateTime now = DateTime.Now;

        int groupLength = groups.Length;

        for (int i = 0, j = 0; i < groupLength; i++, j = 0)
        {
            WordGroup group = groups[i];

            group.Date = now.AddSeconds(i);

            foreach (var word in group.Words)
            {
                j++;

                word.Date = now.AddSeconds(j);
            }
        }
    }

    private static Task<Stream> GetStream()
    {
        const string path = "Json/Dictionaries.json";

        Task<Stream> stream = FileSystem.OpenAppPackageFileAsync(path);

        return stream;
    }
    private static async Task<StreamReader> GetReader()
    {
        Stream stream = await GetStream();

        StreamReader reader = new StreamReader(stream);

        return reader;
    }
}