using System;
using System.Collections.Generic;

namespace OpenDictionary.Models;

public class WordMetadata : IEntity
{
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;

    public ICollection<Phonetic> Phonetics { get; set; } = null!;

    public ICollection<Meaning> Meanings { get; set; } = null!;
}