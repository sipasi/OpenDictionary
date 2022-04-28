
using System.Collections.Generic;

namespace Framework.Words
{
    public interface IMeaning
    {
        public string PartOfSpeech { get; }

        public IEnumerable<IWordDefinition> Definitions { get; }

        public IEnumerable<string> Synonyms { get; }
        public IEnumerable<string> Antonyms { get; }
    }
}
