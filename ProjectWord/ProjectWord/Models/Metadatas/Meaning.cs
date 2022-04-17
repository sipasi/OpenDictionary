using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Framework.Words;

namespace OpenDictionary.Models
{
    public class Meaning : IMeaning, IEntity
    {
        public Guid Id { get; private set; }

        public string PartOfSpeech { get; set; }
        public ICollection<Definition> Definitions { get; set; }

        [NotMapped]
        IEnumerable<IWordDefinition> IMeaning.Definitions => Definitions;
    }
}
