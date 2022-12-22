#nullable enable

using System;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Newtonsoft.Converters;

internal class AntonymConverter : JsonConverter<Antonyms>
{
    public override bool CanWrite => false;

    public override Antonyms? ReadJson(JsonReader reader, Type objectType, Antonyms? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var token = JToken.Load(reader);

        var values = string.Join(", ", token.ToObject<string[]>().Select(value => value));

        Antonyms antonyms = new Antonyms { Value = values };

        return antonyms;
    }

    public override void WriteJson(JsonWriter writer, Antonyms? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}