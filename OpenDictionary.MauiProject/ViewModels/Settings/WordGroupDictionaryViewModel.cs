#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Storage;


using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Loadings;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.ViewModels.Helpers;

namespace OpenDictionary.ViewModels;

public sealed partial class WordGroupDictionaryViewModel
{
    private readonly IDatabaseConnection<AppDatabaseContext> connection;
    private readonly ILoadingMessageService loading;
    private readonly IDialogMessageService dialog;
    private readonly IToastMessageService toast;

    public IAsyncRelayCommand AddPreinstalledCommand { get; }

    public WordGroupDictionaryViewModel(IDatabaseConnection<AppDatabaseContext> connection, IDialogMessageService dialog, IToastMessageService toast, ILoadingMessageService loading)
    {
        this.connection = connection;
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

        await using AppDatabaseContext context = connection.Open();

        foreach (var group in groups)
        {
            var result = await context.WordGroups
                .Select(entity => entity.Name)
                .FirstOrDefaultAsync(entity => entity == group.Name);

            if (result is null)
            {
                count++;

                await context.WordGroups.AddAsync(group);
            }
        }

        await context.SaveChangesAsync();

        string message = count > 0 ? $"{count} dictionaries are installed" : "All dictionaries have already preinstalled";

        await toast.ShowSuccess(message);
    }
    [RelayCommand]
    private async Task DeleteAll()
    {
        DialogResult result = await EntityDeleteDialog.Show(dialog);

        if (result is DialogResult.Accept)
        {
            int count = 0;

            await loading.Show("Deleting", string.Empty, async () =>
            {
                count = await connection.Open(static async context =>
                {
                    var groups = await context.WordGroups.IncludeAll().ToArrayAsync();

                    if (groups.Length is 0)
                    {
                        return 0;
                    }

                    context.WordGroups.RemoveRange(groups);

                    await context.SaveChangesAsync();

                    return groups.Length;
                });
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
        var result = JsonSerializer.Deserialize<WordGroup[]>(json);

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