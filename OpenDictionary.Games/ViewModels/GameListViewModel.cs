using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Databases;
using OpenDictionary.Games.Observables;
using OpenDictionary.Models;
using OpenDictionary.Services.Navigations;
using OpenDictionary.ViewModels;

namespace OpenDictionary.Games.WordConformities.ViewModels;

public class GameListViewModel
{
    private string id;

    private readonly IDatabaseConnection<DbContext> connection;

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

    public GameListViewModel(IDatabaseConnection<DbContext> connection, INavigationService navigation, Routes routes)
    {
        this.id = string.Empty;

        this.connection = connection;
        this.navigation = navigation;

        Games = new CollectionViewModel<GameInfo>(OnLoad, OnTapped);

        var info = new List<GameInfo>()
        {
            new GameInfo
            {
                Image = "icon_origin_to_translation.png",
                Name = "Origin to translation",
                Description = "",
                Route = routes.OriginToTranslation,
                CountToUnlock = 8,
            },
            new GameInfo
            {
                Image = "icon_translation_to_origin.png",
                Name = "Translation to origin",
                Description = "",
                Route = routes.TranslationToOrigin,
                CountToUnlock = 8,
            },
        };

        Games.Collection.AddRange(info);
    }

    public async Task OnLoad()
    {
        Guid guid = Guid.Parse(id);

        var group = await connection.Open(context => context
            .Set<WordGroup>()
            .Select(static x => new { x.Id, x.Words.Count })
            .FirstAsync(x => x.Id == guid)
        );

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

    public readonly struct Routes
    {
        public required string OriginToTranslation { get; init; }
        public required string TranslationToOrigin { get; init; }

        [SetsRequiredMembers]
        public Routes(string originToTranslation, string translationToOrigin)
        {
            OriginToTranslation = originToTranslation;
            TranslationToOrigin = translationToOrigin;
        }
    }
}