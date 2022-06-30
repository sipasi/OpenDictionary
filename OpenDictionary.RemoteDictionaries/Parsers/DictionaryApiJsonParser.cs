#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers.Converters;
using OpenDictionary.RemoteDictionaries.Parsers.Resolvers;

namespace OpenDictionary.RemoteDictionaries.Parsers
{
    /// https://dictionaryapi.dev
    public class DictionaryApiJsonParser : IJsonParser
    {
        public Task<WordMetadata?> Parse(string json)
        {
            bool isArray = IsArray(json);

            try
            {
                WordMetadata? word = isArray
                    ? ParseArray(json)
                    : ParseSingleObject(json);

                return Task.FromResult(word);
            }
            catch (Exception exception)
            {
                string message = GetMessage(exception);

                Debug.WriteLine(message);
            }

            return Task.FromResult<WordMetadata?>(default);
        }

        private bool IsArray(string json)
        {
            const char openArraySymbol = '[';
            const char closeArraySymbol = ']';

            bool isArray = json.StartsWith(openArraySymbol) && json.EndsWith(closeArraySymbol);

            return isArray;
        }

        private WordMetadata? ParseArray(string json)
        {
            WordMetadata[] metadatas = JsonConvert.DeserializeObject<WordMetadata[]>(json, GetSettings())
                ?? Array.Empty<WordMetadata>();

            if (metadatas.Length == 0)
            {
                return null;
            }

            var phoneticComparer = new AudioEqualityComparer();

            string value = metadatas[0].Value;

            IEnumerable<Phonetic> phonetics = metadatas
                .SelectMany(word => word.Phonetics)
                .Distinct(phoneticComparer)
                .Where(phonetic =>
                    string.IsNullOrWhiteSpace(phonetic.Value) is false &&
                    string.IsNullOrWhiteSpace(phonetic.Audio) is false)
                ?? metadatas.Select(word => word.Phonetics).FirstOrDefault();

            IEnumerable<Meaning> meanings = metadatas.SelectMany(word => word.Meanings);

            WordMetadata result = new WordMetadata
            {
                Value = value,
                Meanings = meanings.ToArray(),
                Phonetics = phonetics.ToArray(),
            };

            return result;
        }

        private WordMetadata? ParseSingleObject(string json)
        {
            return JsonConvert.DeserializeObject<WordMetadata>(json, GetSettings());
        }

        private JsonSerializerSettings GetSettings()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new DictionaryApiNameResolver(),
            };

            settings.Converters.Add(new SynonymConverter());
            settings.Converters.Add(new AntonymConverter());

            return settings;
        }

        private string GetMessage(Exception exception)
        {
            string message = exception.Message;
            string? inner = exception.InnerException?.Message;

            return $"{message}. {inner ?? null}";
        }

        private class AudioEqualityComparer : IEqualityComparer<Phonetic>
        {
            public bool Equals(Phonetic first, Phonetic second)
            {
                return first.Audio == second.Audio;
            }

            public int GetHashCode(Phonetic phonetic)
            {
                return phonetic.Audio.GetHashCode();
            }
        }
    }
}