#nullable enable

using System.Text.Json;

using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers.Documents.Readers;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents;

internal readonly ref struct SingleElementParser
{
    private readonly JsonElement element;

    public SingleElementParser(in JsonElement element) => this.element = element;

    public WordMetadata? Parse()
    {
        string? word = element.GetProperty("word").GetString();

        if (string.IsNullOrWhiteSpace(word))
        {
            return default;
        }

        element.TryGetProperty("phonetics", out JsonElement phonetics);
        element.TryGetProperty("meanings", out JsonElement meanings);

        ListReader<PhoneticReader, Phonetic> phoneticsReader = new(ref phonetics);
        ListReader<MeaningReader, Meaning> meaningsReader = new(ref meanings);

        return new WordMetadata
        {
            Value = word,
            Phonetics = phoneticsReader.Read(),
            Meanings = meaningsReader.Read()
        };
    }
}