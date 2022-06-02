#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Converters
{
    internal class SynonymConverter : JsonConverter<IEnumerable<Synonym>>
    {
        public override bool CanWrite => false;

        public override IEnumerable<Synonym>? ReadJson(JsonReader reader, Type objectType, IEnumerable<Synonym>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            var values = token.ToObject<string[]>().Select(value => new Synonym { Value = value });

            return values;
        }

        public override void WriteJson(JsonWriter writer, IEnumerable<Synonym>? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}