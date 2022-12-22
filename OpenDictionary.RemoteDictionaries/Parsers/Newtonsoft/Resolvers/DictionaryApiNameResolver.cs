#nullable enable

using System;
using System.Collections.Generic;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers.Newtonsoft.Resolvers;

internal class DictionaryApiNameResolver : DefaultContractResolver
{
    public static readonly DictionaryApiNameResolver Instance = new DictionaryApiNameResolver();

    private static Dictionary<Type, Func<JsonProperty, JsonProperty>> pairs = new Dictionary<Type, Func<JsonProperty, JsonProperty>>
    {
        [typeof(WordMetadata)] = WordMetadataResolver,
        [typeof(Phonetic)] = PhoneticResolver,
        [typeof(Definition)] = DefinitionResolver,
    };

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);

        Type type = property.DeclaringType!;

        bool contains = pairs.ContainsKey(type);

        if (contains is false)
        {
            return property;
        }

        var resolver = pairs[type];

        return resolver.Invoke(property);
    }

    private static JsonProperty WordMetadataResolver(JsonProperty property)
    {
        const string name = nameof(WordMetadata.Value);
        const string preferred = "Word";

        return SetIfEquals(property, name, preferred);
    }
    private static JsonProperty PhoneticResolver(JsonProperty property)
    {
        const string name = nameof(Phonetic.Value);
        const string preferred = "Text";

        return SetIfEquals(property, name, preferred);
    }
    private static JsonProperty DefinitionResolver(JsonProperty property)
    {
        const string name = nameof(Definition.Value);
        const string preferred = "definition";

        return SetIfEquals(property, name, preferred);
    }

    private static JsonProperty SetIfEquals(JsonProperty property, string name, string preferred)
    {
        bool sameName = Equals(property.PropertyName, name);

        if (sameName)
        {
            property.PropertyName = preferred;
        }

        return property;
    }

    private static bool Equals(string? first, string? second)
    {
        return string.Equals(first, second, StringComparison.OrdinalIgnoreCase);
    }
}