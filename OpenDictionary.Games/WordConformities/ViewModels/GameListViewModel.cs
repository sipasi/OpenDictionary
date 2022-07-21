using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OpenDictionary.Collections.Storages;
using OpenDictionary.Games.WordConformities.Observables;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.Services.Navigations.Routes;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Games.WordConformities.ViewModels;

public class GameListViewModel
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

            Games.LoadCommand?.ExecuteAsync(default);
        }
    }

    public CollectionViewModel<GameInfo> Games { get; }

    public GameListViewModel(IStorage<WordGroup> storage, INavigationService navigation)
    {
        this.id = string.Empty;
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
                Route = AppRoutes.Game.OriginToTranslation,
                CountToUnlock = 8,
            },
            new GameInfo
            {
                Image = "icon_translation_to_origin.png",
                Name = "Translation to origin",
                Description = "",
                Route = AppRoutes.Game.TranslationToOrigin,
                CountToUnlock = 8,
            },
        };

        Games.Collection.AddRange(infos);
    }

    public async Task OnLoad()
    {
        Guid guid = Guid.Parse(id);

        var query = storage
            .Query()
            .Select(x => new { x.Id, x.Words.Count });

        var group = await Task.Run(() => query.First(x => x.Id == guid));

        foreach (var game in Games.Collection)
        {
            game.WordCount = group.Count;
        }
    }

    private Task OnTapped(GameInfo? game)
    {
        return game is null
            ? Task.CompletedTask
            : navigation.GoToAsync(game.Route, nameof(WordGroup.Id), id);
    }
}