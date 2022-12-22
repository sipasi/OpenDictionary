#nullable enable

using Newtonsoft.Json;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Newtonsoft;

internal readonly struct SingleDeserializer
{
    private readonly string json;
    private readonly JsonSerializerSettings settings;

    public SingleDeserializer(string json, JsonSerializerSettings settings)
    {
        this.json = json;
        this.settings = settings;
    }

    public WordMetadata? Deserialize()
    {
        return JsonConvert.DeserializeObject<WordMetadata>(json, settings);
    }
}
