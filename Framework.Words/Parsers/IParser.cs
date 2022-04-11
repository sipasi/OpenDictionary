#nullable enable

using System.Threading.Tasks;

namespace Framework.Words.Parsers
{
    public interface IParser<T>
    {
        Task<IWordMetadata?> Parse(T value);
    }
}
