#nullable enable

using System.Text.Json.Nodes;

namespace OpenDictionary.RemoteDictionaries.Parsers.Nodes.Readers;

internal interface INodeReader<T>
{
    T? Read(JsonNode node);
}