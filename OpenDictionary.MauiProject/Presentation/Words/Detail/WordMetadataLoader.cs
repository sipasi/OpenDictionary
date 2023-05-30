#nullable enable

using System.Linq;
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

            WordMetadata entity = Filter(metadata);

            await context.Set<WordMetadata>().AddAsync(entity);

            await context.SaveChangesAsync();
        }

        return metadata;
    }

    private static WordMetadata Filter(WordMetadata metadata)
    {
        WordMetadata result = new()
        {
            Value = metadata.Value,
            Meanings = metadata.Meanings,
            Phonetics = metadata.Phonetics.Where(IsEligiblePhonetic).ToList(),
        };

        return result;
    }

    private static bool IsEligiblePhonetic(Phonetic phonetic) =>
        string.IsNullOrWhiteSpace(phonetic.Value) is false &&
        string.IsNullOrWhiteSpace(phonetic.Audio) is false;
}