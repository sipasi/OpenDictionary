#nullable enable

using System.Collections.Generic;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers;

internal readonly ref struct MetadataMerger
{
    private readonly string word;
    private readonly int count;
    private readonly IReadOnlyList<WordMetadata> metadatas;

    private readonly bool isEmpty;

    public MetadataMerger(IReadOnlyList<WordMetadata> metadatas)
    {
        this.metadatas = metadatas;

        count = metadatas.Count;

        bool isEmpty = count == 0;

        word = isEmpty ? string.Empty : metadatas[0].Value;
    }

    public WordMetadata? Merge()
    {
        if (isEmpty)
        {
            return null;
        }

        CountCollections(out int phoneticsCount, out int meaningsCount);

        List<Phonetic> phonetics = new(phoneticsCount);
        List<Meaning> meanings = new(meaningsCount);

        for (int i = 0; i < count; i++)
        {
            WordMetadata metadata = metadatas[i];

            phonetics.AddRange(metadata.Phonetics);
            meanings.AddRange(metadata.Meanings);
        }

        return new()
        {
            Value = word,
            Meanings = meanings,
            Phonetics = phonetics
        };
    }

    private void CountCollections(out int phonetics, out int meanings)
    {
        phonetics = meanings = 0;

        for (int i = 0; i < count; i++)
        {
            WordMetadata metadata = metadatas[i];

            phonetics += metadata.Phonetics.Count;

            meanings += metadata.Meanings.Count;
        }
    }
}