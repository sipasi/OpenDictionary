#nullable enable

using System.Collections.Generic;
using System.Text.Json;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents.Readers;

internal readonly struct MeaningReader : IElementReader<Meaning>
{
    public Meaning? Read(in JsonElement element)
    {
        string? partOfSpeech = element.GetProperty("partOfSpeech").GetString();

        if (string.IsNullOrWhiteSpace(partOfSpeech))
        {
            return default;
        }

        element.TryGetProperty("definitions", out var definitionsElement);
        element.TryGetProperty("synonyms", out var synonymsElement);
        element.TryGetProperty("antonyms", out var antonymsElement);

        SynonymsReader synonymsReader;
        AntonymsReader antonymsReader;
        ListReader<DefinitionReader, Definition> listReader = new(in definitionsElement);

        Synonyms? synonyms = synonymsReader.Read(in synonymsElement);
        Antonyms? antonyms = antonymsReader.Read(in antonymsElement);

        List<Definition> definitions = listReader.Read();

        return new()
        {
            PartOfSpeech = partOfSpeech,
            Antonyms = antonyms!,
            Synonyms = synonyms!,
            Definitions = definitions
        };

    }
}