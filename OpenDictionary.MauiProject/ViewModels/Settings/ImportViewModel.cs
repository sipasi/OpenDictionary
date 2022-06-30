#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Maui.Storage;

using MvvmHelpers;

using Newtonsoft.Json;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.XamarinApp.Services.Messages.Toasts;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
public sealed partial class ImportViewModel
{
    private readonly IStorage<WordGroup> storage;
    private readonly INavigationService navigation;
    private readonly IToastMessageService toast;

    public ObservableRangeCollection<WordGroup> Items { get; }
    public ObservableRangeCollection<object> SelectedItems { get; }

    public ImportViewModel(IStorage<WordGroup> storage, INavigationService navigation, IToastMessageService toast)
    {
        this.storage = storage;
        this.navigation = navigation;
        this.toast = toast;

        Items = new();
        SelectedItems = new();
    }

    [RelayCommand]
    private async Task SelectFile()
    {
        var picked = await FilePicker.PickMultipleAsync();

        if (picked is null)
        {
            return;
        }

        var files = picked
            .Where(file => file.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        if (files.Length == 0)
        {
            await toast.ShowError(message: "Please select a correct json files");

            return;
        }

        Items.Clear();
        SelectedItems.Clear();

        foreach (var file in files)
        {
            WordGroup[]? loadled = await LoadFromJson(file.FullPath);

            Items.AddRange(loadled!);
        }
    }

    [RelayCommand]
    private async Task Import()
    {
        var selected = SelectedItems;
        int count = selected.Count;

        if (count == 0)
        {
            _ = toast.ShowSuccess(message: $"Select at least one dictionary");

            return;
        }

        IEnumerable<WordGroup?> import = selected.Select(word => word as WordGroup);

        await storage.AddRangeAsync(import!);

        _ = toast.ShowSuccess(message: $"Imported: {count}");

        await navigation.GoBackAsync();
    }
    private async Task<WordGroup[]?> LoadFromJson(string path)
    {
        StreamReader reader = new(path);

        string json = await reader.ReadToEndAsync();

        WordGroup[]? groups = Deserialize(json);

        if (groups is null)
        {
            await toast.ShowError(message: "Json file have incorrect format");

            return null;
        }
        if (groups.Length is 0)
        {
            await toast.ShowError(message: "Dictionaries are empty");

            return null;
        }

        return groups;
    }

    private static WordGroup[]? Deserialize(string json)
    {
        try
        {
            bool isArray = json.StartsWith('[');

            if (isArray)
            {
                return JsonConvert.DeserializeObject<WordGroup[]>(json);
            }

            var group = JsonConvert.DeserializeObject<WordGroup>(json);

            if (group is not null)
            {
                return new[] { group };
            }
        }
        catch { }

        return null;
    }
}