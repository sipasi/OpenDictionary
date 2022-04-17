
using System.Collections.Generic;

namespace Framework.Words
{
    public interface IMeaning
    {
        public string PartOfSpeech { get; }

        public IEnumerable<IWordDefinition> Definitions { get; }
    }
}
