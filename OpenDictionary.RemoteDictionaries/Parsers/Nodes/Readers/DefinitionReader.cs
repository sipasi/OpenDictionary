#nullable enable

using System.Text.Json.Nodes;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes.Readers;

internal readonly struct DefinitionReader : INodeReader<Definition>
{
    public Definition? Read(JsonNode node)
    {
        JsonNode? definitionNode = node["definition"];
        JsonNode? exampleNode = node["example"];

        string? definition = definitionNode?.GetValue<string>();
        string? example = exampleNode?.GetValue<string>();

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