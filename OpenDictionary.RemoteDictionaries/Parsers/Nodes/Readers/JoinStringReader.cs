#nullable enable

using System.Text;
using System.Text.Json.Nodes;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes.Readers;

public readonly struct JoinStringReader : INodeReader<string>
{
    public string? Read(JsonNode? node)
    {
        string separetor = ", ";

        JsonArray? array = node?.AsArray();

        if (array is null || array.Count == 0)
        {
            return default;
        }

        int count = array.Count;

        int length = GetAllStringLength(array) + separetor.Length * count;

        if (length is 0)
        {
            return default;
        }

        StringBuilder builder = new(length);

        for (int i = 0; i < count; i++)
        {
            var value = array[i]?.GetValue<string>();

            if (string.IsNullOrWhiteSpace(value))
            {
                continue;
            }

            builder.Append(value).Append(separetor);
        }

        builder.Remove(startIndex: builder.Length - separetor.Length, length: separetor.Length);

        return builder.ToString();
    }

    public static int GetAllStringLength(JsonArray array)
    {
        int count = array.Count;

        int length = 0;

        for (int i = 0; i < count; i++)
        {
            string? value = array[i]?.GetValue<string>();

            if (value is null)
            {
                continue;
            }

            length += value.Length;
        }

        return length;
    }
}