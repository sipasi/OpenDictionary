using System;

namespace OpenDictionary.Models
{
    public class Synonym : IEntity
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}
