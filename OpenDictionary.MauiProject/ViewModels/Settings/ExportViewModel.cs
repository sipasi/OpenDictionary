#nullable enable

using System;
using System.Collections.Generic;
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
using OpenDictionary.XamarinApp.Services.Messages.Toasts;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
public sealed partial class ExportViewModel
{
    private readonly IStorage<WordGroup> storage;
    private readonly IToastMessageService toast;

    [ObservableProperty]
    private string? name;

    public ObservableRangeCollection<WordGroupInfo> Items { get; }
    public List<object> SelectedItems { get; }

    public ExportViewModel(IStorage<WordGroup> storage, IToastMessageService toast)
    {
        this.storage = storage;
        this.toast = toast;

        Items = new();
        SelectedItems = new();
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
            .Select(entity => new
            {
                entity.Name,
                Words = entity.Words.Select(word => new
                {
                    word.Origin,
                    word.Translation
                })
            })
            .ToArrayAsync();

        string? title = name;

        if (name is null)
        {
            string groupName = (selected[0] as WordGroupInfo)!.Name!;

            title = count == 1 ? groupName : "Dictionaries";
        }

        string path = Path.Combine(FileSystem.CacheDirectory, $"{title}.json");

        string json = JsonConvert.SerializeObject(groups, Formatting.Indented);

        await WriteJsonFile(path, json);

        ShareFileRequest request = new ShareFileRequest
        {
            Title = title,
            File = new ShareFile(path)
        };

        await Share.RequestAsync(request);
    }
    private static async Task WriteJsonFile(string path, string json)
    {
        StreamWriter file = File.CreateText(path);

        await file.WriteAsync(json);

        await file.FlushAsync();

        await file.DisposeAsync();
    }
}