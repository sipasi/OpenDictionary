#nullable enable

using System.Threading.Tasks;

using OpenDictionary.Collections.Storages.Extensions;
using OpenDictionary.Databases;
using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Sources;

namespace OpenDictionary.Words.ViewModels;

internal readonly struct WordMetadataLoader
{
    private readonly IDatabaseConnection<DatabaseContext> connection;
    private readonly IDictionarySource source;

    public WordMetadataLoader(IDatabaseConnection<DatabaseContext> connection, IDictionarySource source)
    {
        this.source = source;
        this.connection = connection;
    }

    public async ValueTask<WordMetadata?> Load(string origin)
    {
        if (string.IsNullOrWhiteSpace(origin))
        {
            return null;
        }

        WordMetadata? metadata = await GetMetadataFrom(origin);

        return metadata;
    }

    private async Task<WordMetadata?> GetMetadataFrom(string word)
    {
        await using DatabaseContext context = connection.Open();

        WordMetadata? metadata = await context.Set<WordMetadata>()
            .IncludeAll()
            .GetByWord(word);

        if (metadata == null)
        {
            metadata = await source.GetWord(word);

            if (metadata is null)
            {
                return null;
            }

            await context.Set<WordMetadata>().AddAsync(metadata);

            await context.SaveChangesAsync();
        }

        return metadata;
    }
}