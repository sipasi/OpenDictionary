using System;
using System.Collections.Generic;

namespace OpenDictionary.Models;

public class Folder : IEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public List<WordGroup> Groups { get; set; } = null!;

    public Guid? ParentId { get; set; }
    public Folder? Parent { get; set; }

    public List<Folder> Subfolders { get; set; } = null!;
}