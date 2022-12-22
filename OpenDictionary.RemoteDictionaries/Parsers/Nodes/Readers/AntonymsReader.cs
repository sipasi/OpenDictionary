#nullable enable

using System.Text.Json.Nodes;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes.Readers;

internal readonly struct AntonymsReader : INodeReader<Antonyms>
{
    public Antonyms? Read(JsonNode? node)
    {
        JoinStringReader reader;

        string? value = reader.Read(node);

        return value is null ? null : new Antonyms() { Value = value };
    }
}