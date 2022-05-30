using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Framework.Words;

namespace OpenDictionary.Models;

public class WordMetadata : IWordMetadata, IEntity
{
    public Guid Id { get; set; }

    public string Value { get; set; }

    public ICollection<Phonetic> Phonetics { get; set; }

    public ICollection<Meaning> Meanings { get; set; }

    [NotMapped]
    IEnumerable<IPhonetic> IWordMetadata.Phonetics => Phonetics;
    [NotMapped]
    IEnumerable<IMeaning> IWordMetadata.Meanings => Meanings;
}