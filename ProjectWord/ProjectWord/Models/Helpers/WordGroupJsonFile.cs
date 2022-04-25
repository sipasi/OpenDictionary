#nullable enable

using System;
using System.IO;
using System.Reflection;

using Newtonsoft.Json;

namespace OpenDictionary.Models
{
    internal static class WordGroupJsonFile
    {
        private static readonly string embeddedPath = "OpenDictionary.Assets.Json.Dictionaries.json";

        public static WordGroup[]? Load()
        {
            using Stream stream = GetStream();

            using StreamReader reader = new StreamReader(stream);

            string json = reader.ReadToEnd();

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
    }
}