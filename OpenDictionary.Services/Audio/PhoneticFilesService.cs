#nullable enable

using System.IO;
using System.Threading.Tasks;

using OpenDictionary.Services.IO;

namespace OpenDictionary.Services.Audio
{
    public class PhoneticFilesService : IPhoneticFilesService
    {
        private readonly AudioStorage storage;

        public PhoneticFilesService(IAppDirectoryService appDirectory)
        {
            string directory = "Assets/Words/Audio";

            string path = Path.Combine(appDirectory.AppData, directory);

            storage = new AudioStorage(path, extensions: ".mp3");
        }

        public Task<bool> AddFromWebAsync(string address, string name)
        {
            return storage.AddFromWebAsync(address, name);
        }

        public bool Any(string word) => storage.Any(word);

        public bool Contains(string addres, string word) => storage.Contains(addres, word);

        public string? GetFilePath(string addres, string word) => storage.GetFilePath(addres, word);
    }
}