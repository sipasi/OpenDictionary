#nullable enable

using System.Text;
using System.Text.Json;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents.Readers;

internal readonly struct JoinStringReader : IElementReader<string>
{
    public string? Read(in JsonElement element)
    {
        string separetor = ", ";

        int count = element.GetArrayLength();

        if (count == 0)
        {
            return default;
        }

        int length = GetAllStringLength(in element) + separetor.Length * count;

        if (length is 0)
        {
            return default;
        }

        StringBuilder builder = new(length);

        for (int i = 0; i < count; i++)
        {
            var value = element[i].GetString();

            if (string.IsNullOrWhiteSpace(value))
            {
                continue;
            }

            builder.Append(value).Append(separetor);
        }

        builder.Remove(startIndex: builder.Length - separetor.Length, length: separetor.Length);

        return builder.ToString();
    }
    public static int GetAllStringLength(in JsonElement element)
    {
        int count = element.GetArrayLength();

        int length = 0;

        for (int i = 0; i < count; i++)
        {
            string? value = element[i].GetString();

            if (value is null)
            {
                continue;
            }

            length += value.Length;
        }

        return length;
    }
}