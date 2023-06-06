using System;

namespace OpenDictionary.Models;

public class Word : IEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string Origin { get; set; } = null!;
    public string Translation { get; set; } = null!;

    public Guid GroupId { get; set; }
    public WordGroup Group { get; set; } = null!;
}