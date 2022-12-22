#nullable enable

using System.Text.Json;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents.Readers;

internal readonly struct AntonymsReader : IElementReader<Antonyms>
{
    public Antonyms? Read(in JsonElement element)
    {
        JoinStringReader reader;

        string? value = reader.Read(in element);

        return value is null ? null : new Antonyms() { Value = value };
    }
}