#nullable enable

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenDictionary.Services.Audio;

public class AudioDownloader
{
    private readonly HttpClient client = new HttpClient();

    public async Task<bool> Download(DirectoryInfo directory, string address, string? rename = null)
    {
        string name = rename ?? Path.GetFileName(address);

        try
        {
            ReadOnlyMemory<byte> bytes = await client.GetByteArrayAsync(address);

            string path = Path.Combine(directory.FullName, name);

            using FileStream stream = File.Create(path);

            await stream.WriteAsync(bytes);

            await stream.FlushAsync();
        }
        catch (Exception e)
        {
            Debug.WriteLine($"{nameof(AudioDownloader)}. {e.Message}.Inner: {e.InnerException?.Message ?? "Empty"}");

            return false;
        }

        return true;
    }
}