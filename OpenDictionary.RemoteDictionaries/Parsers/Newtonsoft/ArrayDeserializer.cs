#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Newtonsoft;

internal readonly struct ArrayDeserializer
{
    private readonly string json;
    private readonly JsonSerializerSettings settings;

    public ArrayDeserializer(string json, JsonSerializerSettings settings)
    {
        this.json = json;
        this.settings = settings;
    }

    public WordMetadata? Deserialize()
    {
        WordMetadata[] metadatas = JsonConvert.DeserializeObject<WordMetadata[]>(json, settings)
            ?? Array.Empty<WordMetadata>();

        if (metadatas.Length == 0)
        {
            return null;
        }

        PhoneticEqualityComparer equalityComparer = new(metadatas);

        IEnumerable<Phonetic> phonetics = equalityComparer.GetUnique();

        IEnumerable<Meaning> meanings = metadatas.SelectMany(word => word.Meanings);

        WordMetadata result = new()
        {
            Value = metadatas[0].Value,
            Meanings = meanings.ToArray(),
            Phonetics = phonetics.ToArray(),
        };

        return result;
    }
}

file readonly struct PhoneticEqualityComparer
{
    private readonly static AudioEqualityComparer audioEqualityComparer = new();

    private readonly WordMetadata[] metadatas;

    public PhoneticEqualityComparer(WordMetadata[] metadatas)
    {
        this.metadatas = metadatas;
    }

    public IEnumerable<Phonetic> GetUnique()
    {
        return metadatas
            .SelectMany(word => word.Phonetics)
            .Distinct(audioEqualityComparer)
            .Where(phonetic =>
                string.IsNullOrWhiteSpace(phonetic.Value) is false &&
                string.IsNullOrWhiteSpace(phonetic.Audio) is false)

            ?? metadatas.Select(word => word.Phonetics).FirstOrDefault()
            ?? Array.Empty<Phonetic>();
    }
}

file class AudioEqualityComparer : IEqualityComparer<Phonetic>
{
    public bool Equals(Phonetic? first, Phonetic? second)
    {
        if (first == null || second == null)
            return false;

        return first.Audio == second.Audio;
    }

    public int GetHashCode(Phonetic phonetic)
    {
        return phonetic.Audio?.GetHashCode() ?? 0;
    }
}