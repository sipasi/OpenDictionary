#nullable enable

using System;
using System.Text.Json;

using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Logger;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents;

/// https://dictionaryapi.dev
public class DictionaryApiJsonParser : IJsonParser
{
    public WordMetadata? Parse(string json)
    {
        WordMetadata? word = default;

        try
        {
            bool isArray = ParserAnalizer.IsArray(json);

            using JsonDocument document = JsonDocument.Parse(json);

            if (document is null)
            {
                return default;
            }

            JsonElement root = document.RootElement;

            SingleElementParser single = new(in root);
            ArrayElementParser array = new(in root);

            word = isArray ? array.Parse() : single.Parse();
        }
        catch (Exception exception)
        {
            ExceptionLogger.Log(exception);
        }

        return ParserAnalizer.NullIfEmpty(word);
    }
}