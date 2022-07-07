using System;

namespace OpenDictionary.Models;

public class Synonyms : IEntity
{
    public Guid Id { get; set; }

    public string Value { get; set; }
}
