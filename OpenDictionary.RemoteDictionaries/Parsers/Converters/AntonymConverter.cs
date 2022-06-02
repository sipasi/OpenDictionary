#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Converters
{
    internal class AntonymConverter : JsonConverter<IEnumerable<Antonym>>
    {
        public override bool CanWrite => false;

        public override IEnumerable<Antonym>? ReadJson(JsonReader reader, Type objectType, IEnumerable<Antonym>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            var values = token.ToObject<string[]>().Select(value => new Antonym { Value = value });

            return values;
        }

        public override void WriteJson(JsonWriter writer, IEnumerable<Antonym>? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}