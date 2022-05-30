using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Views.Pages;

namespace OpenDictionary.ViewModels.Games;

[QueryProperty(nameof(Id), nameof(Id))]
public class GameListViewModel : ViewModel
{
    private string id;

    private readonly IStorage<WordGroup> storage;

    private readonly INavigationService navigation;

    public string Id
    {
        get => id;
        set
        {
            id = value;

            Games.LoadCommand.ExecuteAsync();
        }
    }

    public CollectionViewModel<GameInfo> Games { get; }

    public GameListViewModel(IStorage<WordGroup> storage, INavigationService navigation)
    {
        this.storage = storage;
        this.navigation = navigation;

        Games = new CollectionViewModel<GameInfo>(OnLoad, OnTapped);

        var infos = new List<GameInfo>()
        {
            new GameInfo
            {
                Image = "icon_origin_to_translation.png",
                Name = "Origin to translation",
                Description = "",
                Page = typeof(GameOriginToTranslationPage),
                CountToUnlock = 8,
            },
            new GameInfo
            {
                Image = "icon_translation_to_origin.png",
                Name = "Translation to origin",
                Description = "",
                Page = typeof(GameTranslationToOriginPage),
                CountToUnlock = 8,
            },
        };

        Games.Collection.AddRange(infos);
    }

    public async Task OnLoad()
    {
        Guid guid = Guid.Parse(id);

        var group = await storage
            .Query()
            .Select(x => new { x.Id, x.Words.Count })
            .FirstAsync(x => x.Id == guid);

        foreach (var game in Games.Collection)
        {
            game.WordCount = group.Count;
        }
    }

    private Task OnTapped(GameInfo info)
    {
        return navigation.GoToAsync(info.Page.Name, nameof(WordGroup.Id), id);
    }
}