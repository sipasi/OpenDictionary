#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using OpenDictionary.Collections.Storages;
using OpenDictionary.DataTransfer;
using OpenDictionary.DataTransfer.Services;
using OpenDictionary.DataTransfer.ViewModels;
using OpenDictionary.Models;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.ViewModels;

public sealed partial class WordGroupExportViewModel : ExportViewModel<WordGroupInfo, WordGroupExportViewModel.WordData>
{
    private readonly IStorage<WordGroup> storage;

    public WordGroupExportViewModel(IStorage<WordGroup> storage, IFileExportService exportService, IFileExporter exporter, INavigationService navigation, IToastMessageService toast)
        : base(exportService, exporter, navigation, toast) => this.storage = storage;

    protected override async ValueTask<WordGroupInfo[]> OnLoad()
    {
        var groups = await storage.Query().Select(group => new WordGroupInfo
        {
            Id = group.Id,
            Name = group.Name,
            Count = group.Words.Count
        }).ToArrayAsync();

        return groups;
    }

    protected override ValueTask<FileData<WordData>[]> PrepareExport()
    {
        var selected = Collection.SelectedItems;
        int count = selected.Count;

        if (count == 0)
        {
            return ValueTask.FromResult(Array.Empty<FileData<WordData>>());
        }

        var hash = selected
            .Select(item => (item as WordGroupInfo)!.Id)
            .ToHashSet();

        var groups = storage
            .Query()
            .Where(entity => hash.Contains(entity.Id))
            .Select(static entity => new FileData<WordData>
            {
                Name = entity.Name,
                Datas = entity.Words.Select(word => new WordData
                {
                    Origin = word.Origin,
                    Translation = word.Translation
                }).ToArray()
            })
            .ToArrayAsync();

        return new(groups);
    }

    public readonly struct WordData
    {
        public required string Origin { get; init; }
        public required string Translation { get; init; }
    }
}