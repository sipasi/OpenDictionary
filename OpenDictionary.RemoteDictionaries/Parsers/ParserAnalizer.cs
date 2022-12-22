#nullable enable

using OpenDictionary.Models;

namespace OpenDictionary.RemoteDictionaries.Parsers;

internal static class ParserAnalizer
{
    public static bool IsArray(string json)
    {
        const char openArraySymbol = '[';
        const char closeArraySymbol = ']';

        bool isArray = json.StartsWith(openArraySymbol) && json.EndsWith(closeArraySymbol);

        return isArray;
    }

    public static WordMetadata? NullIfEmpty(WordMetadata? word)
    {
        if (word is null || IsEmpty(word))
        {
            return null;
        }

        return word;
    }

    private static bool IsEmpty(WordMetadata word)
    {
        return word.Meanings is null && word.Phonetics is null && word.Value is null;
    }
}
