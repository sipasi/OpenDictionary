#nullable enable

using System.Collections.Generic;
using System.Text.Json.Nodes;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes.Readers;

internal readonly struct MeaningReader : INodeReader<Meaning>
{
    public Meaning? Read(JsonNode node)
    {
        string? partOfSpeech = node["partOfSpeech"]?.GetValue<string>();

        if (string.IsNullOrWhiteSpace(partOfSpeech))
        {
            return default;
        }

        SynonymsReader synonymsReader;
        AntonymsReader antonymsReader;
        ListReader<DefinitionReader, Definition> listReader = new(node["definitions"]);

        Synonyms? synonyms = synonymsReader.Read(node["synonyms"]);
        Antonyms? antonyms = antonymsReader.Read(node["antonyms"]);

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