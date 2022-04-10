
namespace Framework.Words
{
    public interface IMeaning
    {
        public string PartOfSpeech { get; }

        public IDefinition[] Definitions { get; }
    }
}
