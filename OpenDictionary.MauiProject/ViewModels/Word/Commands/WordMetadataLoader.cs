﻿#nullable enable

using System.Threading.Tasks;

using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Sources;

namespace OpenDictionary.ViewModels.Words.Commands;

internal sealed class WordMetadataLoader
{
    private readonly WordDetailViewModel viewModel;
    private readonly IDictionarySource source;
    private readonly IDatabaseConnection<AppDatabaseContext> connection;

    public WordMetadataLoader(WordDetailViewModel viewModel, IDictionarySource source, IDatabaseConnection<AppDatabaseContext> connection)
    {
        this.viewModel = viewModel;
        this.source = source;
        this.connection = connection;
    }

    public async Task Load()
    {
        if (string.IsNullOrWhiteSpace(viewModel.Word.Origin))
        {
            return;
        }

        viewModel.Metadata.Clear();

        WordMetadata? metadata = await GetMetadataFrom(viewModel.Word.Origin);

        if (metadata is null)
        {
            return;
        }

        viewModel.Metadata.Set(metadata);
    }

    private async Task<WordMetadata?> GetMetadataFrom(string word)
    {
        await using AppDatabaseContext context = connection.Open();

        WordMetadata? metadata = await context.WordMetadatas
            .IncludeAll()
            .GetByWord(word);

        if (metadata == null)
        {
            metadata = await source.GetWord(word);

            if (metadata is null)
            {
                return null;
            }

            await context.WordMetadatas.AddAsync(metadata);

            await context.SaveChangesAsync();
        }

        return metadata;
    }
}