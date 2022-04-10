#nullable enable

using System;
using System.IO;
using System.Threading.Tasks;

using Framework.Words;
using Framework.Words.Parsers;

using NUnit.Framework;

namespace ProjectWord.Words.Tests
{
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

            IWordMetadata? definition = await parser.Parse(json);

            Assert.IsNotNull(definition);

            string word = InsistWordJson.Word;

            Assert.AreEqual(definition!.Value, word);
        }
    }
}