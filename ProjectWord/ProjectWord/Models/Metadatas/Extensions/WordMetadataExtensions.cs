using System.Linq;

using Framework.Words;

namespace OpenDictionary.Models.Extensions
{
    public static class WordMetadataExtensions
    {
        public static WordMetadata Clone(this IWordMetadata metadata)
        {
            WordMetadata clone = new WordMetadata
            {
                Value = metadata.Value,
            };

            clone.Phonetics = metadata.Phonetics.Select(phonetic => new Phonetic
            {
                Value = phonetic.Value,
                Audio = phonetic.Audio,
            }).ToList();

            clone.Meanings = metadata.Meanings.Select(meaning => new Meaning
            {
                PartOfSpeech = meaning.PartOfSpeech,

                Synonyms = meaning.Synonyms.Select(entity => new Synonym { Value = entity }).ToList(),
                Antonyms = meaning.Antonyms.Select(entity => new Antonym { Value = entity }).ToList(),

                Definitions = meaning.Definitions.Select(definition => new Definition
                {
                    Value = definition.Value,
                    Example = definition.Example,
                }).ToList(),
            }).ToList();

            return clone;
        }
    }
}