#nullable enable

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Storage;

using MvvmHelpers;

using Newtonsoft.Json;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Toasts;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
public sealed partial class ExportViewModel
{
    private readonly IStorage<WordGroup> storage;
    private readonly IToastMessageService toast;

    [ObservableProperty]
    private string? name;
    [ObservableProperty]
    private bool multiple;

    private bool MultipleExport => CanMultipleExport && multiple;

    public bool CanMultipleExport => SelectedItems.Count > 1;

    public ObservableRangeCollection<WordGroupInfo> Items { get; }
    public ObservableCollection<object> SelectedItems { get; }

    public ExportViewModel(IStorage<WordGroup> storage, IToastMessageService toast)
    {
        this.storage = storage;
        this.toast = toast;

        Items = new();
        SelectedItems = new();

        SelectedItems.CollectionChanged += (_, _) =>
        {
            OnPropertyChanged(nameof(CanMultipleExport));
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

        Items.AddRange(groups);
    }

    [RelayCommand]
    private async Task Export()
    {
        var selected = SelectedItems;
        int count = selected.Count;

        if (count == 0)
        {
            await toast.Show(message: "Select at least one dictionary");
            return;
        }

        var hash = selected.Select(item => (item as WordGroupInfo)!.Id).ToHashSet();

        var groups = await storage
            .Query()
            .Where(entity => hash.Contains(entity.Id))
            .Select(entity => new GroupData
            {
                Name = entity.Name,
                Words = entity.Words.Select(word => new WordData
                {
                    Origin = word.Origin,
                    Translation = word.Translation
                }).ToArray()
            }).ToArrayAsync();

        Task task = MultipleExport
            ? AsMultipleFiles(groups)
            : AsSingleFile(groups);

        await task;
    }

    private static async Task AsSingleFile(GroupData[] datas)
    {
        int count = datas.Length;

        string? title = "Dictionaries";

        if (title is null)
        {
            var info = datas[0];

            string groupName = info.Name;

            title = count == 1 ? groupName : "Dictionaries";
        }

        string path = await CreateCacheFile(datas, title);

        var request = new ShareFileRequest
        {
            Title = title,
            File = new ShareFile(path)
        };

        await Share.RequestAsync(request);

        DeleteCacheFileByName(title);
    }
    private static async Task AsMultipleFiles(GroupData[] datas)
    {
        var tasks = datas.Select(async data =>
        {
            string path = await CreateCacheFile(data, data.Name);

            ShareFile file = new ShareFile(path);

            return file;
        });

        var files = await Task.WhenAll(tasks);

        var request = new ShareMultipleFilesRequest
        {
            Title = "Dictionaries",
            Files = files.ToList()
        };

        await Share.RequestAsync(request);

        DeleteCacheFiles(files);
    }

    private static async Task<string> CreateCacheFile<T>(T data, string name)
    {
        string path = Path.Combine(FileSystem.CacheDirectory, $"{name}.json");

        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        await WriteJsonFile(path, json);

        return path;
    }

    private static void DeleteCacheFiles(params ShareFile[] files)
    {
        foreach (var file in files)
        {
            DeleteCacheFileByName(file.FileName);
        }
    }
    private static void DeleteCacheFileByName(string name)
    {
        string path = Path.Combine(FileSystem.CacheDirectory, $"{name}.json");

        if (File.Exists(path) is false)
        {
            return;
        }

        File.Delete(path);
    }

    private async Task ExportRequest<T>(T data, ShareRequestBase request, string name)
    {
        string path = await CreateCacheFile(data, name);

        Task? task = request switch
        {
            ShareFileRequest fileRequest => Share.RequestAsync(fileRequest),
            ShareMultipleFilesRequest filesRequest => Share.RequestAsync(filesRequest),
            _ => null
        };

        if (task is null)
        {
            string message = $"[{GetType()}] have not supported the {request?.GetType()} request type";

            throw new NotSupportedException(message);
        }

        await task;

        DeleteCacheFileByName(name);
    }

    private static async Task WriteJsonFile(string path, string json)
    {
        StreamWriter file = File.CreateText(path);

        await file.WriteAsync(json);

        await file.FlushAsync();

        await file.DisposeAsync();
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