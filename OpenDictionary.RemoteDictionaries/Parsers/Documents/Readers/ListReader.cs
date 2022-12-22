#nullable enable

using System.Collections.Generic;
using System.Text.Json;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents.Readers;

internal readonly ref struct ListReader<TReader, T> where TReader : IElementReader<T>, new()
{
    private readonly ref JsonElement source;

    public ListReader(ref JsonElement source) => this.source = ref source;

    public List<T> Read()
    {
        int count = source.GetArrayLength();

        if (count == 0)
        {
            return new(capacity: 0);
        }

        List<T> list = new(count);

        TReader reader = new();

        for (int i = 0; i < count; i++)
        {
            JsonElement element = source[i];

            T? value = reader.Read(in element);

            if (value is null)
            {
                continue;
            }

            list.Add(value);
        }

        return list;
    }
}