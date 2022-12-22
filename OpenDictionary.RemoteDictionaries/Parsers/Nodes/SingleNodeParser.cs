#nullable enable

using System.Text.Json.Nodes;

using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers.Nodes.Readers;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes;

internal readonly ref struct SingleNodeParser
{
    private readonly JsonNode? node;

    public SingleNodeParser(JsonNode? node) => this.node = node;

    public WordMetadata? Parse()
    {
        if (node is null)
        {
            return default;
        }

        string? word = node["word"]?.GetValue<string>();

        if (string.IsNullOrWhiteSpace(word))
        {
            return default;
        }

        ListReader<PhoneticReader, Phonetic> phoneticsReader = new(node["phonetics"]);
        ListReader<MeaningReader, Meaning> meaningsReader = new(node["meanings"]);

        return new WordMetadata
        {
            Value = word,
            Phonetics = phoneticsReader.Read(),
            Meanings = meaningsReader.Read()
        };
    }
}