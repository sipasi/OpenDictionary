using System;

namespace OpenDictionary.Models;

public class Phonetic : IEntity
{
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;
    public string? Audio { get; set; }

    public Guid MetadataId { get; set; }
    public WordMetadata Metadata { get; set; } = null!;
}