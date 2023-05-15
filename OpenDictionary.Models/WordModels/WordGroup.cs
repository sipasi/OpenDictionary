using System;
using System.Collections.Generic;

namespace OpenDictionary.Models;

public class WordGroup : IEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string Name { get; set; }

    public string OriginCulture { get; set; }
    public string TranslationCulture { get; set; }

    public ICollection<Word> Words { get; set; }
}