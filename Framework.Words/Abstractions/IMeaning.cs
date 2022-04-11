
namespace Framework.Words
{
    public interface IMeaning
    {
        public string PartOfSpeech { get; }

        public IWordDefinition[] Definitions { get; }
    }
}
