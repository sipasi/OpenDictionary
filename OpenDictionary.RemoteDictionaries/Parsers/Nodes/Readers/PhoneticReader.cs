#nullable enable

using System.Text.Json.Nodes;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes.Readers;

internal readonly struct PhoneticReader : INodeReader<Phonetic>
{
    public Phonetic? Read(JsonNode node)
    {
        string? audio = node["audio"]?.GetValue<string>();
        string? text = node["text"]?.GetValue<string>();

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