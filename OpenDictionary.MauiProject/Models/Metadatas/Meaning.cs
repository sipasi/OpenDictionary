using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;

using Framework.Words;

namespace OpenDictionary.Models;

public class Meaning : IMeaning, IEntity
{
    public Guid Id { get; set; }

    public string PartOfSpeech { get; set; }
    public ICollection<Definition> Definitions { get; set; }

    public ICollection<Synonym> Synonyms { get; set; }

    public ICollection<Antonym> Antonyms { get; set; }

    [NotMapped, DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IEnumerable<string> IMeaning.Synonyms => Synonyms.Select(item => item.Value);
    [NotMapped, DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IEnumerable<string> IMeaning.Antonyms => Antonyms.Select(item => item.Value);

    [NotMapped, DebuggerBrowsable(DebuggerBrowsableState.Never)]
    IEnumerable<IWordDefinition> IMeaning.Definitions => Definitions;
}
