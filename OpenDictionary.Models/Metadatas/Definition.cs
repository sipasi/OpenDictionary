using System;

namespace OpenDictionary.Models
{
    public class Definition : IEntity
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
        public string Example { get; set; }
    }
}