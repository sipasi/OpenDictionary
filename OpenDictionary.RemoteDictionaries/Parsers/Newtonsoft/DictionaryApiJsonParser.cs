#nullable enable

using System;

using Newtonsoft.Json;

using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Logger;
using OpenDictionary.RemoteDictionaries.Parsers.Newtonsoft.Converters;
using OpenDictionary.RemoteDictionaries.Parsers.Newtonsoft.Resolvers;

namespace OpenDictionary.RemoteDictionaries.Parsers.Newtonsoft;

/// https://dictionaryapi.dev
public class DictionaryApiJsonParser : IJsonParser
{
    private static readonly JsonSerializerSettings settings = new()
    {
        ContractResolver = new DictionaryApiNameResolver(),
        Converters =
        {
            new SynonymConverter(),
            new AntonymConverter()
        }
    };

    public WordMetadata? Parse(string json)
    {
        bool isArray = ParserAnalizer.IsArray(json);

        WordMetadata? word = default;

        try
        {
            SingleDeserializer singleDeserializer = new(json, settings);
            ArrayDeserializer arrayDeserializer = new(json, settings);

            word = isArray
                ? arrayDeserializer.Deserialize()
                : singleDeserializer.Deserialize();
        }
        catch (Exception exception)
        {
            ExceptionLogger.Log(exception);
        }

        return ParserAnalizer.NullIfEmpty(word);
    }
}