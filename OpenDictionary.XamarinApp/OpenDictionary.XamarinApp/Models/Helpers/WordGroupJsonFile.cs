#nullable enable

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OpenDictionary.Models
{
    internal static class WordGroupJsonFile
    {
        private static readonly string embeddedPath = "OpenDictionary.XamarinApp.Assets.Json.Dictionaries.json";

        public static WordGroup[]? Load()
        {
            using StreamReader reader = GetReader();

            string json = reader.ReadToEnd();

            return DeserializeAndFillDates(json);
        }

        public static async Task<WordGroup[]?> LoadAsync()
        {
            using StreamReader reader = GetReader();

            string json = await reader.ReadToEndAsync();

            return DeserializeAndFillDates(json);
        }

        public static WordGroup[]? DeserializeAndFillDates(string json)
        {
            var result = JsonConvert.DeserializeObject<WordGroup[]>(json);

            FillDates(result);

            return result;
        }

        private static void FillDates(WordGroup[]? groups)
        {
            if (groups is null)
            {
                return;
            }

            DateTime now = DateTime.Now;

            int groupLength = groups.Length;

            for (int i = 0, j = 0; i < groupLength; i++, j = 0)
            {
                WordGroup group = groups[i];

                group.Date = now.AddSeconds(i);

                foreach (var word in group.Words)
                {
                    j++;

                    word.Date = now.AddSeconds(j);
                }
            }
        }

        private static Stream GetStream()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(WordGroupJsonFile)).Assembly;

            Stream stream = assembly.GetManifestResourceStream(embeddedPath);

            return stream;
        }
        private static StreamReader GetReader()
        {
            Stream stream = GetStream();

            StreamReader reader = new StreamReader(stream);

            return reader;
        }
    }
}