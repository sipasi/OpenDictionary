using System;

namespace OpenDictionary.Models
{
    public class Phonetic : IEntity
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
        public string? Audio { get; set; }
    }
}