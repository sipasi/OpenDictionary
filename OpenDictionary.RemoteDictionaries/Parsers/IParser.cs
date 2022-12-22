#nullable enable

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers;

public interface IParser<T>
{
    WordMetadata? Parse(T value);
}
