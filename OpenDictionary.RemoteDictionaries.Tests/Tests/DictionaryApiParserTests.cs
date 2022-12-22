#nullable enable

using OpenDictionary.Models;
using OpenDictionary.RemoteDictionaries.Parsers;

namespace OpenDictionary.Words.Tests;

public abstract class DictionaryApiParserTests<TParcer> where TParcer : IJsonParser, new()
{
    private static WordMetadata correct;

    [SetUp]
    public void SetUp()
    {
        correct = new WordMetadata
        {
            Value = "insist",
            Phonetics = new List<Phonetic>
            {
            new()
            {
                Value = "/ɪnˈsɪst/",
                Audio = "https://api.dictionaryapi.dev/media/pronunciations/en/insist-us.mp3"
            }
            },
            Meanings = new List<Meaning>
            {
                new Meaning
                {
                    PartOfSpeech = "verb",
                    Synonyms = new Synonyms
                    {
                        Value= "synonyms, synonyms, synonyms",
                    },
                    Antonyms = new Antonyms
                    {
                        Value="antonyms, antonyms, antonyms"
                    },
                    Definitions = new List<Definition>()
                    {
                        new Definition()
                        {
                            Value = "(with on or upon or (that + ordinary verb form)) To hold up a claim emphatically.",
                            Example = "I insist that my secretary dresses nicely."
                        },
                        new Definition()
                        {
                            Value = "(sometimes with on or upon or (that + subjunctive)) To demand continually that something happen or be done.",
                            Example = "I insist that my secretary dress nicely."
                        },
                        new Definition()
                        {
                            Value = "To stand (on); to rest (upon); to lean (upon).",
                            Example = ""
                        },
                    },
                }
            },
        };
    }

    [Test]
    public ValueTask InsistEmptyObject() => ParserTest(InsistWordJson.LoadEmptyObject, AssertEmptyObject);
    [Test]
    public ValueTask InsistEmptyObjectWithScopes() => ParserTest(InsistWordJson.LoadEmptyObjectWithScopes, AssertEmptyObject);
    [Test]
    public ValueTask InsistWithScopes() => ParserTest(InsistWordJson.LoadWithScopes, AssertCorrectObject);
    [Test]
    public ValueTask InsistWithNullProperties() => ParserTest(InsistWordJson.LoadWithNullProperties, AssertWithNullProperties);
    [Test]
    public ValueTask InsistWithoutScopes() => ParserTest(InsistWordJson.LoadWithoutScopes, AssertCorrectObject);

    public async ValueTask ParserTest(Func<ValueTask<string>> fileLoading, Action<WordMetadata?> assert)
    {
        var parser = new TParcer();

        string json = await fileLoading.Invoke();

        WordMetadata? definition = parser.Parse(json);

        assert.Invoke(definition);
    }
    private static void AssertEmptyObject(WordMetadata? metadata)
    {
        Assert.That(metadata, Is.Null);
    }
    private static void AssertWithNullProperties(WordMetadata? metadata)
    {
        Assert.That(metadata, Is.Not.Null);

        Assert.That(metadata.Value, Is.EqualTo("insist"));
    }
    private static void AssertCorrectObject(WordMetadata? metadata)
    {
        Assert.That(metadata, Is.Not.Null);

        Assert.That(metadata.Value, Is.EqualTo(correct.Value));

        AssertCollections(metadata.Phonetics, correct.Phonetics,
            (left, right) => left.Value == right.Value && left.Audio == right.Audio);

        AssertCollections(metadata.Meanings, correct.Meanings, static (left, right) =>
            left.Antonyms.Value == right.Antonyms.Value &&
            left.Synonyms.Value == right.Synonyms.Value &&
            left.PartOfSpeech == right.PartOfSpeech &&
            AssertCollections(left.Definitions, right.Definitions, static (left, right) =>
                (left.Example == right.Example || (string.IsNullOrWhiteSpace(left.Example) && string.IsNullOrWhiteSpace(right.Example))) &&
                left.Value == right.Value));
    }

    private static bool AssertCollections<T>(ICollection<T> first, ICollection<T> second, Func<T, T, bool> compare)
    {
        var firstArray = first.ToArray();
        var secondArray = second.ToArray();

        Assert.That(firstArray.Length, Is.EqualTo(secondArray.Length),
            message: $"ICollection<{typeof(T).Name}>");

        for (int i = 0; i < firstArray.Length; i++)
        {
            Assert.That(
                compare.Invoke(firstArray[i], secondArray[i]), Is.True,
                message: $"ICollection<{typeof(T).Name}>, index: {i}");
        }

        return true;
    }
}