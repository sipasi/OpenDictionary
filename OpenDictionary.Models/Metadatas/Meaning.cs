using System;
using System.Collections.Generic;


namespace OpenDictionary.Models
{
    public class Meaning : IEntity
    {
        public Guid Id { get; set; }

        public string PartOfSpeech { get; set; }

        public IEnumerable<Definition> Definitions { get; set; }

        public IEnumerable<Synonym> Synonyms { get; set; }

        public IEnumerable<Antonym> Antonyms { get; set; }
    }
}
