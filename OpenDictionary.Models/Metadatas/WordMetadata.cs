using System;
using System.Collections.Generic;

namespace OpenDictionary.Models
{
    public class WordMetadata : IEntity
    {
        public Guid Id { get; set; }

        public string Value { get; set; }

        public IEnumerable<Phonetic> Phonetics { get; set; }

        public IEnumerable<Meaning> Meanings { get; set; }
    }
}