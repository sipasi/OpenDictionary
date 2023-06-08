using System;

namespace OpenDictionary.Models;

public class Synonyms : IEntity
{
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;

    public Guid MeaningId { get; set; }
    public Meaning Meaning { get; set; } = null!;
}
