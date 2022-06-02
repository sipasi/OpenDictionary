#nullable enable

using System.Threading.Tasks;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers
{
    public interface IParser<T>
    {
        Task<WordMetadata?> Parse(T value);
    }
}
