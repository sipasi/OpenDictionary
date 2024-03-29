﻿using System.Text.Json;

using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Databases;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels;

namespace OpenDictionary.DataTransfer.ViewModels;

public partial class ImportJsonViewModel<T> : BindableObject where T : class
{
    private readonly IDatabaseConnection<DbContext> connection;
    private readonly INavigationService navigation;
    private readonly IToastMessageService toast;

    public string FileExtension { get; }

    public SelectableCollectionViewModel<T> Collection { get; }

    public ImportJsonViewModel(IDatabaseConnection<DbContext> connection, INavigationService navigation, IToastMessageService toast)
    {
        this.connection = connection;

        this.navigation = navigation;

        this.toast = toast;

        FileExtension = "json";

        Collection = new();
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
            .Where(file => file.FileName.EndsWith(FileExtension, StringComparison.OrdinalIgnoreCase))
            .ToArray();

        if (files.Length == 0)
        {
            await toast.ShowError(message: $"Please select a correct {FileExtension} file");

            return;
        }

        Collection.Clear();

        foreach (var file in files)
        {
            T[]? loadled = await LoadFromJson(file.FullPath);

            Collection.AddRange(loadled!);
        }
    }

    [RelayCommand]
    private async Task Import()
    {
        var selected = Collection.SelectedItems;
        int count = selected.Count;

        if (count == 0)
        {
            await toast.ShowSuccess(message: $"Select at least one item");

            return;
        }

        try
        {
            await using DbContext context = connection.Open();

            IEnumerable<T?> import = selected.Select(word => word as T);

            await context.AddRangeAsync(import!);

            await context.SaveChangesAsync();

            await toast.ShowSuccess($"Imported: {count}");
        }
        catch (Exception e)
        {
            await toast.ShowError($"Cant import. Error - {e.Message}");
        }

        await navigation.GoBackAsync();
    }
    private async ValueTask<T[]?> LoadFromJson(string path)
    {
        StreamReader reader = new(path);

        string json = await reader.ReadToEndAsync();

        T[]? groups = Deserialize(json);

        if (groups is null)
        {
            await toast.ShowError(message: $"{FileExtension} file have incorrect format");

            return null;
        }
        if (groups.Length is 0)
        {
            await toast.ShowError(message: "File are empty");

            return null;
        }

        return groups;
    }

    private static T[]? Deserialize(string json)
    {
        try
        {
            bool isArray = json.StartsWith('[');

            if (isArray)
            {
                return JsonSerializer.Deserialize<T[]>(json);
            }

            var group = JsonSerializer.Deserialize<T>(json);

            if (group is not null)
            {
                return new[] { group };
            }
        }
        catch { }

        return null;
    }
}