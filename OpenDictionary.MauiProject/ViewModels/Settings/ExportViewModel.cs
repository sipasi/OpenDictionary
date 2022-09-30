#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Storage;

using Newtonsoft.Json;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.DataTransfer;
using OpenDictionary.Services.Messages.Toasts;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
public sealed partial class ExportViewModel
{
    private static readonly string defaultName = "Dictionaries";

    private readonly IStorage<WordGroup> storage;
    private readonly IFileExportService export;
    private readonly IToastMessageService toast;

    public bool CanMultipleExport => Collection.SelectedItems.Count > 1;
    public bool AtLeastOneSelected => Collection.SelectedItems.Count > 0;

    public SelectableCollectionViewModel<WordGroupInfo> Collection { get; }

    public ExportViewModel(IStorage<WordGroup> storage, IFileExportService export, IToastMessageService toast)
    {
        this.storage = storage;
        this.export = export;
        this.toast = toast;

        Collection = new()
        {
            SelectionsChanged = () =>
            {
                OnPropertyChanged(nameof(CanMultipleExport));
                OnPropertyChanged(nameof(AtLeastOneSelected));
            }
        };
    }

    [RelayCommand]
    private async Task Load()
    {
        var groups = await storage.Query().Select(group => new WordGroupInfo
        {
            Id = group.Id,
            Name = group.Name,
            Count = group.Words.Count
        }).ToArrayAsync();

        Collection.Clear();

        Collection.AddRange(groups);
    }

    [RelayCommand]
    private Task ExportAsSingle() => TryExport(Share.AsSingle);
    [RelayCommand]
    private Task ExportAsMultiple() => TryExport(Share.AsMultiple);

    private async Task TryExport(Func<IFileExportService, GroupData[], Task> func)
    {
        GroupData[] groups = await PrepareExport();

        if (groups.Length == 0)
        {
            await toast.Show(message: "Select at least one dictionary");

            return;
        }

        await func.Invoke(export, groups);
    }

    private Task<GroupData[]> PrepareExport()
    {
        var selected = Collection.SelectedItems;
        int count = selected.Count;

        if (count == 0)
        {
            return Task.FromResult(Array.Empty<GroupData>());
        }

        var hash = selected
            .Select(item => (item as WordGroupInfo)!.Id)
            .ToHashSet();

        var groups = storage
            .Query()
            .Where(entity => hash.Contains(entity.Id))
            .Select(static entity => new GroupData
            {
                Name = entity.Name,
                Words = entity.Words.Select(word => new WordData
                {
                    Origin = word.Origin,
                    Translation = word.Translation
                }).ToArray()
            })
            .ToArrayAsync();

        return groups;
    }

    private static class Share
    {
        public static async Task AsSingle(IFileExportService export, GroupData[] groups)
        {
            int count = groups.Length;

            string? title = count is 1
                ? groups[0].Name
                : defaultName;

            string path = await Cache.Create(groups, title);

            await export.AsSingleFile(title, path);

            Cache.Delete(title);
        }
        public static async Task AsMultiple(IFileExportService export, GroupData[] groups)
        {
            var task = groups.Select(data => Cache.Create(data, data.Name));

            var paths = await Task.WhenAll(task);

            await export.AsMultipleFiles(title: "Dictionaries", paths);

            Cache.DeleteRange(paths);
        }
    }

    private static class Cache
    {
        public static async Task<string> Create<T>(T data, string name)
        {
            string path = Path.Combine(FileSystem.CacheDirectory, $"{name}.json");

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            await Json.Write(path, json);

            return path;
        }

        public static void DeleteRange(Span<string> files)
        {
            foreach (var file in files)
            {
                Delete(file);
            }
        }
        public static void Delete(string path)
        {
            if (File.Exists(path) is false)
            {
                return;
            }

            File.Delete(path);
        }
    }

    private static class Json
    {
        public static async Task Write(string path, string json)
        {
            StreamWriter file = File.CreateText(path);

            await file.WriteAsync(json);

            await file.FlushAsync();

            await file.DisposeAsync();
        }
    }

    private readonly struct GroupData
    {
        public string Name { get; init; }
        public WordData[] Words { get; init; }
    }
    private readonly struct WordData
    {
        public string Origin { get; init; }
        public string Translation { get; init; }
    }
}