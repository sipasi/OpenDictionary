using System.Threading.Tasks;
namespace Framework.Words.DictionarySources
{
    public interface IDictionarySource
    {
        ValueTask<IWordMetadata?> GetWord(string value);
    }
}
