#nullable enable

using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Essentials;

namespace OpenDictionary.Services.Audio
{
    internal class PhoneticStorageService : IPhoneticStorageService
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string directory = Path.Combine(FileSystem.AppDataDirectory, "Assets/Words/Phonetics");
        private static readonly string extension = ".mp3";

        public PhoneticStorageService()
        {
            if (Directory.Exists(directory) is false)
            {
                Directory.CreateDirectory(directory);
            }
        }

        public async ValueTask<bool> AddFromWebAsync(string address, string name)
        {
            string addressExtension = Path.GetExtension(address).ToLower();

            if (string.Equals(extension, addressExtension) is false)
            {
                return false;
            }

            string path = GetFilePath(name);

            try
            {
                ReadOnlyMemory<byte> bytes = await client.GetByteArrayAsync(address);

                using FileStream stream = File.Create(path);

                await stream.WriteAsync(bytes);

                await stream.FlushAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{nameof(PhoneticStorageService)}. {e.Message}");

                return false;
            }

            return true;
        }

        public bool Contains(string name)
        {
            string file = GetFilePath(name);

            return File.Exists(file);
        }

        public string GetFilePath(string name)
        {
            return Path.Combine(directory, CombineExtension(name.ToLower()));
        }

        private string CombineExtension(string name)
        {
            return $"{name}{extension}";
        }
    }
}