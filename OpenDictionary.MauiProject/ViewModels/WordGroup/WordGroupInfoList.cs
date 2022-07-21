using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;

namespace OpenDictionary.ViewModels;

[INotifyPropertyChanged]
public sealed partial class WordGroupInfoList
{
    private readonly IStorage<WordGroup> storage;
    private readonly INavigationService navigation;

    public CollectionViewModel<WordGroupInfo> Groups { get; }

    public WordGroupInfoList(IStorage<WordGroup> storage, INavigationService navigation)
    {
        this.storage = storage;
        this.navigation = navigation;

        Groups = new(Load, Tapped);
    }

    private async Task Load()
    {
        Groups.IsBusy = true;

        var groups = await storage.Query().OrderByDateDescending().Select(group => new WordGroupInfo
        {
            Id = group.Id,
            Name = group.Name,
            Count = group.Words.Count,
        }).ToArrayAsync();

        Groups.Collection.Clear();

        Groups.Collection.AddRange(groups);

        Groups.IsBusy = false;
    }

    [RelayCommand]
    private Task RedirectToCreate()
    {
        return navigation.GoToAsync(AppRoutes.WordGroup.Create);
    }

    private Task Tapped(WordGroupInfo? item)
    {
        if (item is null) return Task.CompletedTask;

        return navigation.GoToAsync(AppRoutes.WordGroup.Detail, parameter: nameof(WordGroup.Id), value: item.Id.ToString()!);
    }
}