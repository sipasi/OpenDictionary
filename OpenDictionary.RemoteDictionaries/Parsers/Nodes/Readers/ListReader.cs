#nullable enable

using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes.Readers;

internal readonly struct ListReader<TReader, T> where TReader : INodeReader<T>, new()
{
    private readonly JsonNode? source;

    public ListReader(JsonNode? source) => this.source = source;

    public List<T> Read()
    {
        JsonArray? array = source?.AsArray();

        if (array is null)
        {
            return new(capacity: 0);
        }

        int count = array.Count;

        if (count == 0)
        {
            return new(capacity: 0);
        }

        List<T> list = new(count);

        TReader reader = new();

        for (int i = 0; i < count; i++)
        {
            JsonNode? node = array[i];

            if (node is null)
            {
                continue;
            }

            T? value = reader.Read(node);

            if (value is null)
            {
                continue;
            }

            list.Add(value);
        }

        return list;
    }
}