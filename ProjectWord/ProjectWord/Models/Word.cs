using System;

using Framework.Words;

namespace ProjectWord.Models
{
    public class Word : IEntity
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public string Origin { get; set; }
        public string Translation { get; set; }
    }

    public class WordMetadata : IWordMetadata, IEntity
    {
        public Guid Id { get; }

        public Guid WordId { get; set; }

        public string Value { get; set; }
        public IPhonetic[] Phonetics { get; set; }
        public IMeaning[] Meanings { get; set; }
    }
    public class Phonetic : IPhonetic, IEntity
    {
        public Guid Id { get; }

        public string Value { get; set; }
        public string Audio { get; set; }
    }

    public class Meaning : IMeaning, IEntity
    {
        public Guid Id { get; }

        public string PartOfSpeech { get; set; }
        public IWordDefinition[] Definitions { get; set; }
    }
    public class Definition : IWordDefinition, IEntity
    {
        public Guid Id { get; }

        public string Value { get; set; }
        public string Example { get; set; }
        public string[] Synonyms { get; set; }
        public string[] Antonyms { get; set; }
    }
}
