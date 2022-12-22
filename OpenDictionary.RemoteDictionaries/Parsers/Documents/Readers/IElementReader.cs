#nullable enable

using System.Text.Json;

namespace OpenDictionary.RemoteDictionaries.Parsers.Documents.Readers;

public interface IElementReader<T>
{
    T? Read(in JsonElement element);
}