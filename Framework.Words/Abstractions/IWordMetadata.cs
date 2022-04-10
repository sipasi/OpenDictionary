
namespace Framework.Words
{
    public interface IWordMetadata
    {
        public string Value { get; }

        public IPhonetic[] Phonetics { get; }

        public IMeaning[] Meanings { get; }
    }
}
