#nullable enable

using System;
using System.Text.Json.Nodes;

using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Logger;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes;

/// https://dictionaryapi.dev
public class DictionaryApiJsonParser : IJsonParser
{
    public WordMetadata? Parse(string json)
    {
        WordMetadata? word = default;

        try
        {
            bool isArray = ParserAnalizer.IsArray(json);

            JsonNode? node = JsonNode.Parse(json);

            if (node is null)
            {
                return default;
            }

            word = isArray ? ParceArrayNode(node.AsArray()) : ParceSingleNode(node);
        }
        catch (Exception exception)
        {
            ExceptionLogger.Log(exception);
        }

        return ParserAnalizer.NullIfEmpty(word);
    }

    private static WordMetadata? ParceSingleNode(JsonNode node)
    {
        SingleNodeParser parser = new(node);

        return parser.Parse();
    }
    private static WordMetadata? ParceArrayNode(JsonArray array)
    {
        ArrayNodeParser parser = new(array);

        return parser.Parse();
    }
}