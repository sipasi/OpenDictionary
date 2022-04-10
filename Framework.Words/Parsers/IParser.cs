#nullable enable

using System.Threading.Tasks;

namespace Framework.Words.Parsers
{
    public interface IParser<T>
    {
        ValueTask<IWordMetadata?> Parse(T value);
    }
}
