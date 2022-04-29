using System;

using Framework.Words;

namespace OpenDictionary.Models
{
    public class Phonetic : IPhonetic, IEntity
    {
        public Guid Id { get; set; }

        public string Value { get; set; }
        public string Audio { get; set; }
    }
}
