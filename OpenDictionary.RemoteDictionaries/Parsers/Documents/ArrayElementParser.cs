#nullable enable

using System.Collections.Generic;
using System.Text.Json;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents;

internal readonly ref struct ArrayElementParser
{
    private readonly JsonElement element;

    public ArrayElementParser(in JsonElement element) => this.element = element;

    public WordMetadata? Parse()
    {
        string? name = element[0].GetProperty("word").ToString();

        if (string.IsNullOrWhiteSpace(name))
        {
            return default;
        }

        int count = element.GetArrayLength();

        List<WordMetadata> metadatas = new(count);

        for (int i = 0; i < count; i++)
        {
            JsonElement item = element[i];

            SingleElementParser singleNode = new(item);

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