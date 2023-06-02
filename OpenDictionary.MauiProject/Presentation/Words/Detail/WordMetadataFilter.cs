#nullable enable

using System;
using System.Linq;

using OpenDictionary.Models;

namespace OpenDictionary.Words.ViewModels;

public readonly struct WordMetadataFilter
{
    private readonly WordMetadata metadata;

    public WordMetadataFilter(WordMetadata metadata) => this.metadata = metadata;

    public WordMetadata Filter()
    {
        WordMetadata result = new()
        {
            Value = metadata.Value,
            Meanings = metadata.Meanings
                .Where(static meaning => meaning.PartOfSpeech is not null)
                .GroupBy(static meaning => meaning.PartOfSpeech)
                .Select(static group => new Meaning
                {
                    PartOfSpeech = group.Key,
                    Synonyms = ExtractSynonyms(group)!,
                    Antonyms = ExtractAntonyms(group)!,
                    Definitions = group
                        .Where(static meaning => meaning.Definitions?.Count != 0)
                        .SelectMany(static meaning => meaning.Definitions)
                        .ToList(),
                }).ToList(),
            Phonetics = metadata.Phonetics
                .Where(IsEligiblePhonetic)
                .DistinctBy(static phonetic => (phonetic.Value, phonetic.Audio))
                .ToList(),
        };

        return result;
    }

    private static Synonyms? ExtractSynonyms(IGrouping<string, Meaning> group)
    {
        if (ConcatString(group, static group => group.Synonyms?.Value) is not string text)
        {
            return null;
        }

        return new()
        {
            Value = text
        };
    }
    private static Antonyms? ExtractAntonyms(IGrouping<string, Meaning> group)
    {
        if (ConcatString(group, static group => group.Antonyms?.Value) is not string text)
        {
            return null;
        }

        return new()
        {
            Value = text
        };
    }
    private static string? ConcatString(IGrouping<string, Meaning> group, Func<Meaning, string?> selector)
    {
        string[] array = group
            .Select(selector.Invoke)
            .Where(static synonym => string.IsNullOrWhiteSpace(synonym) is false)
            .ToArray()!;

        if (array.Length == 0)
        {
            return null;
        }

        return string.Join(", ", array);
    }

    private static bool IsEligiblePhonetic(Phonetic phonetic)
    {
        bool result = string.IsNullOrWhiteSpace(phonetic.Value) is false &&
                      string.IsNullOrWhiteSpace(phonetic.Audio) is false;

        return result;
    }
}