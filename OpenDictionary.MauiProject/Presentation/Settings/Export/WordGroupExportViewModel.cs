﻿#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Databases;
using OpenDictionary.DataTransfer;
using OpenDictionary.DataTransfer.Services;
using OpenDictionary.DataTransfer.ViewModels;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Settings.ViewModels;

public sealed partial class WordGroupExportViewModel : ExportViewModel<WordGroupInfo, WordGroupExportViewModel.WordsInfo>
{
    private readonly IDatabaseConnection<AppDatabaseContext> connection;

    public WordGroupExportViewModel(IDatabaseConnection<AppDatabaseContext> connection, IFileExportService exportService, IFileExporter exporter, INavigationService navigation, IToastMessageService toast)
        : base(exportService, exporter, navigation, toast) => this.connection = connection;

    protected override async ValueTask<WordGroupInfo[]> OnLoad()
    {
        var groups = await connection.Open(context => context.WordGroups.Select(group => new WordGroupInfo
        {
            Id = group.Id,
            Name = group.Name,
            Count = group.Words.Count
        }).ToArrayAsync());

        return groups;
    }

    protected override async ValueTask<FileData<WordsInfo>[]> PrepareExport()
    {
        var selected = Collection.SelectedItems;
        int count = selected.Count;

        if (count == 0)
        {
            return Array.Empty<FileData<WordsInfo>>();
        }

        var hash = selected
            .Select(item => (item as WordGroupInfo)!.Id)
            .ToHashSet();

        var groups = await connection.Open(context => context.WordGroups
            .Where(entity => hash.Contains(entity.Id))
            .Select(static entity => new FileData<WordsInfo>
            {
                Name = entity.Name,
                Data = new WordsInfo
                {
                    Name = entity.Name,
                    Words = entity.Words.Select(word => new WordInfo
                    {
                        Origin = word.Origin,
                        Translation = word.Translation
                    }).ToArray()
                }
            }).ToArrayAsync());

        return groups;
    }

    public readonly struct WordsInfo
    {
        public required string Name { get; init; }
        public required WordInfo[] Words { get; init; }
    }

    public readonly struct WordInfo
    {
        public required string Origin { get; init; }
        public required string Translation { get; init; }
    }
}