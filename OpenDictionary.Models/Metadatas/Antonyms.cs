using System;

namespace OpenDictionary.Models
{
    public class Antonyms : IEntity
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}
