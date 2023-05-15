using System.Linq;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;

namespace OpenDictionary.ViewModels.WordGroups;

public sealed partial class WordGroupInfoList : ObservableObject
{
    private readonly IDatabaseConnection<AppDatabaseContext> connection;
    private readonly INavigationService navigation;

    public CollectionViewModel<WordGroupInfo> Groups { get; }

    public WordGroupInfoList(IDatabaseConnection<AppDatabaseContext> connection, INavigationService navigation)
    {
        this.connection = connection;
        this.navigation = navigation;

        Groups = new(Load, Tapped);
    }

    private async Task Load()
    {
        Groups.IsBusy = true;

        var groups = await connection.Open(context => context.WordGroups.OrderByDateDescending().Select(group => new WordGroupInfo
        {
            Id = group.Id,
            Name = group.Name,
            Count = group.Words.Count,
        }).ToArrayAsync());

        Groups.Collection.Clear();

        Groups.Collection.AddRange(groups);

        Groups.IsBusy = false;
    }

    [RelayCommand]
    private Task RedirectToCreate() => navigation.GoToAsync(AppRoutes.WordGroup.Create);
     
    private Task Tapped(WordGroupInfo? item) => item is null
        ? Task.CompletedTask
        : navigation.GoToAsync(AppRoutes.WordGroup.Detail, parameter: nameof(WordGroup.Id), value: item.Id.ToString()!);

}