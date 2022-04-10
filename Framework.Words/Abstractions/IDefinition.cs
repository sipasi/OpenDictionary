
namespace Framework.Words
{
    public interface IDefinition
    {
        public string Value { get; }
        public string Example { get; }
        public string[] Synonyms { get; }
        public string[] Antonyms { get; }
    }
}
