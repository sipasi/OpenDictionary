#nullable enable

using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers;

namespace OpenDictionary.Words.Tests;

public class DictionaryApiParserTests
{
    [Test]
    public async ValueTask DictionaryApiJsonParserInsistWithScopesTest()
    {
        await ParserTest(InsistWordJson.LoadWithScopes);
    }
    [Test]
    public async ValueTask DictionaryApiJsonParserInsistWithoutScopesTest()
    {
        await ParserTest(InsistWordJson.LoadWithoutScopes);
    }
    public async ValueTask ParserTest(Func<ValueTask<string>> fileLoading)
    {
        var parser = new DictionaryApiJsonParser();

        string json = await fileLoading.Invoke();

        WordMetadata? definition = await parser.Parse(json);

        Assert.That(definition, Is.Not.Null);


        Assert.That(definition.Meanings.Count(), Is.Not.Zero);
        Assert.That(definition.Phonetics.Count(), Is.Not.Zero);

        foreach (var meaning in definition.Meanings)
        {
            Assert.That(meaning.PartOfSpeech, Is.Not.Null);
            Assert.That(meaning.Synonyms, Is.Not.Zero);
            Assert.That(meaning.Synonyms, Is.Not.Zero);
        }
        foreach (var phonetic in definition.Phonetics)
        {
            Assert.That(phonetic.Value, Is.Not.Null);
            Assert.That(phonetic.Audio, Is.Not.Null);
        }

        Assert.That(definition!.Value, Is.Not.Null);
    }
}