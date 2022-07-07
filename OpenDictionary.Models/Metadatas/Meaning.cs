using System;
using System.Collections.Generic;


namespace OpenDictionary.Models;

public class Meaning : IEntity
{
    public Guid Id { get; set; }

    public string PartOfSpeech { get; set; }

    public ICollection<Definition> Definitions { get; set; }

    public Synonyms Synonyms { get; set; }

    public Antonyms Antonyms { get; set; }
}
