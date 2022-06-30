#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Converters
{
    internal class SynonymConverter : JsonConverter<Synonyms>
    {
        public override bool CanWrite => false;

        public override Synonyms? ReadJson(JsonReader reader, Type objectType, Synonyms? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            var values = string.Join(", ", token.ToObject<string[]>().Select(value => value));

            Synonyms synonyms = new Synonyms { Value = values };

            return synonyms;
        }

        public override void WriteJson(JsonWriter writer, Synonyms? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}