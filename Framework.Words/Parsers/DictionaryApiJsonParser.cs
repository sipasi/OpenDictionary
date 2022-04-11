#nullable enable

using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Framework.Words.Parsers
{
    /// https://dictionaryapi.dev
    public class DictionaryApiJsonParser : IJsonParser
    {
        public Task<IWordMetadata?> Parse(string json)
        {
            var value = SolveArraySymbols(json);

            try
            {
                IWordMetadata? word = JsonConvert.DeserializeObject<Word>(value);

                return Task.FromResult(word);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return Task.FromResult<IWordMetadata?>(default);
        }

        private string SolveArraySymbols(string text)
        {
            const char openArraySymbol = '[';
            const char closeArraySymbol = ']';

            if (text.StartsWith(openArraySymbol) && text.EndsWith(closeArraySymbol))
            {
                return text.Remove(startIndex: text.Length - 1, count: 1).Remove(startIndex: 0, count: 1);
            }

            return text;
        }

        private class Word : IWordMetadata
        {
            [JsonProperty("word")]
            public string Value { get; }
            [JsonProperty("phonetics")]
            public IPhonetic[] Phonetics { get; }
            public IMeaning[] Meanings { get; }

            public Word(string value, Phonetic[] phonetics, Meaning[] meanings)
            {
                Value = value;
                Phonetics = phonetics;
                Meanings = meanings;
            }

            public override string ToString()
            {
                return $"Value: {Value}, Phonetics: {Phonetics.Length}, Meanings: {Meanings.Length}";
            }
        }

        private class Phonetic : IPhonetic
        {
            [JsonProperty("text")]
            public string Value { get; }
            [JsonProperty("audio")]
            public string Audio { get; }

            public Phonetic(string value, string audio)
            {
                Value = value;
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
            public IWordDefinition[] Definitions { get; }

            public Meaning(string partOfSpeech, Definition[] definitions)
            {
                PartOfSpeech = partOfSpeech;
                Definitions = definitions;
            }

            public override string ToString()
            {
                return $"PartOfSpeech: {PartOfSpeech}, Definitions: {Definitions.Length}";
            }
        }

        public class Definition : IWordDefinition
        {
            [JsonProperty("definition")]
            public string Value { get; }
            public string Example { get; }
            public string[] Synonyms { get; }
            public string[] Antonyms { get; }

            public Definition(string value, string? example, string[] synonyms, string[] antonyms)
            {
                Value = value;
                Example = example ?? string.Empty;
                Synonyms = synonyms;
                Antonyms = antonyms;
            }

            public override string ToString()
            {
                return $"Value: {Value}, Example: {Example}, Synonyms: {Synonyms.Length}, Antonyms: {Antonyms.Length}";
            }
        }
    }
}
