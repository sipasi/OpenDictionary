using System.Threading.Tasks;
namespace Framework.Words.DictionarySources
{
    public interface IDictionarySource
    {
        Task<IWordMetadata?> GetWord(string value);
    }
}
