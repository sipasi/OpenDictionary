#nullable enable

using System.Text.Json;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents.Readers;

internal readonly struct DefinitionReader : IElementReader<Definition>
{
    public Definition? Read(in JsonElement element)
    {
        string? definition = element.TryGetProperty("definition", out var definitionElement) ? definitionElement.GetString() : default;
        string? example = element.TryGetProperty("example", out var exampleElement) ? exampleElement.GetString() : default;

        if (string.IsNullOrWhiteSpace(definition) && string.IsNullOrWhiteSpace(example))
        {
            return default;
        }

        return new()
        {
            Value = definition!,
            Example = example!,
        };
    }
}