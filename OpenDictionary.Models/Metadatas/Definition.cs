using System;

namespace OpenDictionary.Models;

public class Definition : IEntity
{
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;
    public string? Example { get; set; }

    public Guid MeaningId { get; set; }
    public Meaning Meaning { get; set; } = null!;
}