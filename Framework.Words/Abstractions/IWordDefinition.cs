
namespace Framework.Words
{
    public interface IWordDefinition
    {
        public string Value { get; }
        public string Example { get; }
        public string[] Synonyms { get; }
        public string[] Antonyms { get; }
    }
}
