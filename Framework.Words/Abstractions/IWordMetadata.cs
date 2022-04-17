using System.Collections.Generic;

namespace Framework.Words
{
    public interface IWordMetadata
    {
        public string Value { get; }

        public IEnumerable<IPhonetic> Phonetics { get; }

        public IEnumerable<IMeaning> Meanings { get; }
    }
}
