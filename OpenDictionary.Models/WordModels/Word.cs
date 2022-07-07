using System;

namespace OpenDictionary.Models;

public class Word : IEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string Origin { get; set; }
    public string Translation { get; set; }
}