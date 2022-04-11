using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Framework.Words.Parsers;
namespace Framework.Words.DictionarySources
{
    /// https://dictionaryapi.dev
    public class DictionaryApiRemoteSource : IDictionarySource
    {
        private readonly string source;

        private readonly HttpClient client;

        private readonly IJsonParser parser;

        public DictionaryApiRemoteSource(IJsonParser parser)
        {
            source = "https://api.dictionaryapi.dev/api/v2/entries/en/";
            client = new HttpClient();
            this.parser = parser;
        }

        public async Task<IWordMetadata?> GetWord(string value)
        {
            Uri uri = GetUri(value);

            string response = await client.GetStringAsync(uri);

            IWordMetadata? result = await parser.Parse(response);

            return result;
        }

        private Uri GetUri(string word)
        {
            string path = Path.Combine(source, word);

            Uri uri = new Uri(path);

            return uri;
        }
    }
}
