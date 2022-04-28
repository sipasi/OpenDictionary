#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Framework.Words.Parsers
{
    /// https://dictionaryapi.dev
    public class DictionaryApiJsonParser : IJsonParser
    {
        public Task<IWordMetadata?> Parse(string json)
        {
            try
            {
                bool isArray = IsArray(json);

                IWordMetadata? word = isArray
                    ? ParseArray(json)
                    : ParseSingleObject(json);

                return Task.FromResult(word);
            }
            catch (Exception exception)
            {
                string message = GetMessage(exception);

                Debug.WriteLine(message);
            }

            return Task.FromResult<IWordMetadata?>(default);
        }

        private bool IsArray(string json)
        {
            const char openArraySymbol = '[';
            const char closeArraySymbol = ']';

            bool isArray = json.StartsWith(openArraySymbol) && json.EndsWith(closeArraySymbol);

            return isArray;
        }

        private IWordMetadata? ParseArray(string json)
        {
            Word[] words = JsonConvert.DeserializeObject<Word[]>(json) ?? Array.Empty<Word>();

            if (words.Length == 0)
            {
                return null;
            }

            var phoneticComparer = new PhoneticComparer();

            string value = words[0].Value;

            IEnumerable<IPhonetic> phonetics = words
                .SelectMany(word => word.Phonetics)
                .Distinct(phoneticComparer)
                .Where(phonetic =>
                    string.IsNullOrWhiteSpace(phonetic.Value) is false &&
                    string.IsNullOrWhiteSpace(phonetic.Audio) is false)
                ?? words.Select(word => word.Phonetics).FirstOrDefault();

            IEnumerable<IMeaning> meanings = words.SelectMany(word => word.Meanings);

            Word result = new Word(value, phonetics, meanings);

            return result;
        }

        private IWordMetadata? ParseSingleObject(string json)
        {
            return JsonConvert.DeserializeObject<Word>(json);
        }

        private string GetMessage(Exception exception)
        {
            string message = exception.Message;
            string? inner = exception.InnerException?.Message;

            return $"{message}. {inner ?? null}";
        }

        private class Word : IWordMetadata
        {
            public string Value { get; }
            public IEnumerable<IPhonetic> Phonetics { get; }
            public IEnumerable<IMeaning> Meanings { get; }

            public Word(string word, IEnumerable<Phonetic> phonetics, IEnumerable<Meaning> meanings)
            {
                Value = word;
                Phonetics = phonetics;
                Meanings = meanings;
            }
            internal Word(string word, IEnumerable<IPhonetic> phonetics, IEnumerable<IMeaning> meanings)
            {
                Value = word;
                Phonetics = phonetics;
                Meanings = meanings;
            }

            public override string ToString()
            {
                return $"Value: {Value}";
            }
        }

        private class Phonetic : IPhonetic
        {
            public string Value { get; }
            public string Audio { get; }

            public Phonetic(string text, string audio)
            {
                Value = text;
                Audio = audio;
            }

            public override string ToString()
            {
                return $"Value: {Value}, Audio: {Audio}";
            }
        }

        private class Meaning : IMeaning
        {
            public string PartOfSpeech { get; }
            public IEnumerable<IWordDefinition> Definitions { get; }
            public IEnumerable<string> Synonyms { get; }
            public IEnumerable<string> Antonyms { get; }

            public Meaning(string partOfSpeech, Definition[] definitions, IEnumerable<string> synonyms, IEnumerable<string> antonyms)
            {
                PartOfSpeech = partOfSpeech;
                Definitions = definitions;
                Synonyms = synonyms;
                Antonyms = antonyms;
            }

            public override string ToString()
            {
                return $"PartOfSpeech: {PartOfSpeech}";
            }
        }

        public class Definition : IWordDefinition
        {
            public string Value { get; }
            public string Example { get; }

            public Definition(string definition, string? example)
            {
                Value = definition;
                Example = example ?? string.Empty;
            }

            public override string ToString()
            {
                return $"Value: {Value}, Example: {Example}";
            }
        }

        private class PhoneticComparer : IEqualityComparer<IPhonetic>
        {
            public bool Equals(IPhonetic first, IPhonetic second)
            {
                return first.Value == second.Value &&
                       first.Audio == second.Audio;
            }

            public int GetHashCode(IPhonetic phonetic)
            {
                return (phonetic.Value, phonetic.Value).GetHashCode();
            }
        }
    }
}
