#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Storage;

using MvvmHelpers.Commands;

using Newtonsoft.Json;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.XamarinApp.Services.Messages.Toasts;

namespace OpenDictionary.ViewModels;

public class ImportExportViewModel : ViewModel
{
    private readonly IStorage<WordGroup> storage;
    private readonly IToastMessageService toast;

    public AsyncCommand<ShareOptions> ShareCommand { get; }
    public AsyncCommand ImportCommand { get; }

    public ImportExportViewModel(IStorage<WordGroup> storage, IToastMessageService toast)
    {
        this.storage = storage;
        this.toast = toast;

        ShareCommand = new AsyncCommand<ShareOptions>(OnShare);

        ImportCommand = new AsyncCommand(OnImport);
    }

    private async Task OnImport()
    {
        FileResult? picked = await FilePicker.PickAsync();

        if (picked is null)
        {
            return;
        }

        if (picked.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase) is false)
        {
            await toast.ShowError(message: "Please select a json file");

            return;
        }

        await toast.ShowAfter(() => ImportFromJson(picked.FullPath));
    }
    private async ValueTask<bool> ImportFromJson(string path)
    {
        StreamReader reader = new StreamReader(path);

        string json = await reader.ReadToEndAsync();

        WordGroup[]? groups = default;

        try
        {
            bool isArray = json.StartsWith('[');

            if (isArray)
            {
                groups = JsonConvert.DeserializeObject<WordGroup[]>(json);
            }

            var group = JsonConvert.DeserializeObject<WordGroup>(json);

            if (group is not null)
            {
                groups = new[]
                {
                    group
                };
            }
        }
        catch { }

        if (groups is null)
        {
            await toast.ShowError(message: "Json file have incorrect format");

            return false;
        }
        if (groups.Length is 0)
        {
            await toast.ShowError(message: "Dictionaries are empty");

            return false;
        }

        return await storage.AddRangeAsync(groups);
    }

    private async Task OnShare(ShareOptions share)
    {
        var groups = await storage.Query().Select(group => new
        {
            Name = group.Name,
            Words = group.Words.Select(word => new
            {
                Origin = word.Origin,
                Translation = word.Translation
            })
        }).ToArrayAsync();

        string json = JsonConvert.SerializeObject(groups, Formatting.Indented);

        if (share is ShareOptions.JsonFile)
        {
            string path = Path.Combine(FileSystem.CacheDirectory, "WordGroupArray.json");

            await WriteJsonFile(path, json);

            ShareFileRequest request = new ShareFileRequest
            {
                Title = "Dictionaries",
                File = new ShareFile(path)
            };

            await Share.RequestAsync(request);
        }
    }
    private static async Task WriteJsonFile(string path, string json)
    {
        StreamWriter file = File.CreateText(path);

        await file.WriteAsync(json);

        await file.FlushAsync();

        await file.DisposeAsync();
    }
}