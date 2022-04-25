
using System.Collections.Generic;

namespace Framework.Words
{
    public interface IWordDefinition
    {
        public string Value { get; }
        public string Example { get; }
        public IEnumerable<string> Synonyms { get; }
        public IEnumerable<string> Antonyms { get; }
    }
}