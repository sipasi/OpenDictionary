using System;
using System.Collections.Generic;


namespace OpenDictionary.Models;

public class Meaning : IEntity
{
    public Guid Id { get; set; }

    public string PartOfSpeech { get; set; } = null!;

    public ICollection<Definition> Definitions { get; set; } = null!;

    public Synonyms Synonyms { get; set; } = null!;

    public Antonyms Antonyms { get; set; } = null!;

    public Guid MetadataId { get; set; }
    public WordMetadata Metadata { get; set; } = null!;
}