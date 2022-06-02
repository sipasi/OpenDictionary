using System.Threading.Tasks;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Sources
{
    public interface IDictionarySource
    {
        Task<WordMetadata?> GetWord(string value);
    }
}
