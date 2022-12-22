#nullable enable

using System.Text.Json;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents.Readers;

internal readonly struct SynonymsReader : IElementReader<Synonyms>
{
    public Synonyms? Read(in JsonElement element)
    {
        JoinStringReader reader;

        string? value = reader.Read(element);

        return value is null ? null : new Synonyms() { Value = value };
    }
}