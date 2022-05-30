using System;

using Framework.Words;

namespace OpenDictionary.Models;

public class Definition : IWordDefinition, IEntity
{
    public Guid Id { get; set; }

    public string Value { get; set; }
    public string Example { get; set; }
}