using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

using Framework.Words;

namespace OpenDictionary.Models
{
    public class Definition : IWordDefinition, IEntity
    {
        public Guid Id { get; private set; }

        public string Value { get; set; }
        public string Example { get; set; }

        public ICollection<Synonym> Synonyms { get; set; }

        public ICollection<Antonym> Antonyms { get; set; }

        [NotMapped]
        IEnumerable<string> IWordDefinition.Synonyms => Synonyms.Select(item => item.Value);
        [NotMapped]
        IEnumerable<string> IWordDefinition.Antonyms => Antonyms.Select(item => item.Value);
    }
}