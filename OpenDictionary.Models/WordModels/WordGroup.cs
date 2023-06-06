using System;
using System.Collections.Generic;

namespace OpenDictionary.Models;

public class WordGroup : IEntity
{
    public Guid Id { get; set; }

    public DateTime Date { get; set; }

    public string Name { get; set; } = null!;

    public string OriginCulture { get; set; } = null!;
    public string TranslationCulture { get; set; } = null!;

    public ICollection<Word> Words { get; set; } = null!;

    public Guid? FolderId { get; set; } = null!;
    public Folder? Folder { get; set; } = null!;
}