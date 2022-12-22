#nullable enable

using System.Text.Json;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents.Readers;

internal readonly struct PhoneticReader : IElementReader<Phonetic>
{
    public Phonetic? Read(in JsonElement element)
    {
        string? audio = element.GetProperty("audio").GetString();
        string? text = element.GetProperty("text").GetString();

        if (string.IsNullOrWhiteSpace(audio) && string.IsNullOrWhiteSpace(text))
        {
            return default;
        }

        return new Phonetic
        {
            Value = text!,
            Audio = audio,
        };
    }
}