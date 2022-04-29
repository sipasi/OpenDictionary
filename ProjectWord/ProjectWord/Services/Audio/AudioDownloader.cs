#nullable enable

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OpenDictionary.Services.Audio
{
    internal class AudioDownloader
    {
        private readonly HttpClient client = new HttpClient();

        private readonly Regex illegal;

        public AudioDownloader()
        {
            string illegalCharacters = new string(Path.GetInvalidFileNameChars());

            illegal = new Regex(string.Format("[{0}]", Regex.Escape(illegalCharacters)));
        }

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
                Debug.WriteLine($"{nameof(PhoneticFilesService)}. {e.Message}.Inner: {e.InnerException?.Message ?? "Empty"}");

                return false;
            }

            return true;
        }

        private string GetLegalName(string name)
        {
            return illegal.Replace(name, "");
        }
    }
}