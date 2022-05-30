#nullable enable

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Maui.Storage;

using Newtonsoft.Json;

namespace OpenDictionary.Models;

internal static class WordGroupJsonFile
{
    private static readonly string embeddedPath = "OpenDictionary.XamarinApp.Assets.Json.Dictionaries.json";

    public static async Task<WordGroup[]?> Load()
    {
        using StreamReader reader = await GetReader();

        string json = reader.ReadToEnd();

        return DeserializeAndFillDates(json);
    }

    public static async Task<WordGroup[]?> LoadAsync()
    {
        using StreamReader reader = await GetReader();

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

    private static Task<Stream> GetStream()
    {
        Task<Stream> stream = FileSystem.OpenAppPackageFileAsync("Json/Dictionaries.json");

        return stream;
    }
    private static async Task<StreamReader> GetReader()
    {
        Stream stream = await GetStream();

        StreamReader reader = new StreamReader(stream);

        return reader;
    }
}