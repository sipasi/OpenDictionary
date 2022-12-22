#nullable enable

using System.Collections.Generic;
using System.Text.Json.Nodes;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes;

internal readonly ref struct ArrayNodeParser
{
    private readonly JsonArray? array;

    public ArrayNodeParser(JsonArray? array) => this.array = array;

    public WordMetadata? Parse()
    {
        if (array is null)
        {
            return default;
        }

        string? name = array[0]?["word"]?.GetValue<string>();

        if (string.IsNullOrWhiteSpace(name))
        {
            return default;
        }

        int count = array.Count;

        List<WordMetadata> metadatas = new(count);

        for (int i = 0; i < count; i++)
        {
            JsonNode? node = array[i];

            SingleNodeParser singleNode = new(node);

            WordMetadata? metadata = singleNode.Parse();

            if (metadata is null)
            {
                continue;
            }

            metadatas.Add(metadata);
        }

        MetadataMerger merger = new(metadatas);

        return merger.Merge();
    }
}