using System;

namespace OpenDictionary.Models
{
    public class Antonym : IEntity
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}
