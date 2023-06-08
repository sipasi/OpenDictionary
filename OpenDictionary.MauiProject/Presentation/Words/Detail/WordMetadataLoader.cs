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

    private async ValueTask<WordMetadata?> GetMetadataFrom(string word) => await GetFromDatabase(word) ?? await GetFromWeb(word);

    private async ValueTask<WordMetadata?> GetFromDatabase(string word)
    {
        await using DatabaseContext context = connection.Open();

        WordMetadata? metadata = await context.Set<WordMetadata>()
           .IncludeAll()
           .GetByWord(word);

        return metadata;
    }
    private async ValueTask<WordMetadata?> GetFromWeb(string word)
    {
        if (await source.GetWord(word) is not WordMetadata metadata)
        {
            return null;
        }

            await context.Set<WordMetadata>().AddAsync(metadata);

            await context.SaveChangesAsync();
        }

    private async ValueTask SaveToDatabase(WordMetadata metadata)
    {
        await using DatabaseContext context = connection.Open();

        await context
            .Set<WordMetadata>()
            .AddAsync(metadata);

        await context.SaveChangesAsync();
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