#nullable enable

using System.Threading.Tasks;

namespace OpenDictionary.Services.Audio;

internal class PhoneticFilesService : IPhoneticFilesService
{
    private readonly AudioStorage storage;

    public PhoneticFilesService()
    {
        string directory = "Assets/Words/Audio";

        storage = new AudioStorage(directory, extensions: ".mp3");
    }

    public Task<bool> AddFromWebAsync(string address, string name)
    {
        return storage.AddFromWebAsync(address, name);
    }

    public bool Any(string word) => storage.Any(word);

    public bool Contains(string addres, string word) => storage.Contains(addres, word);

    public string? GetFilePath(string addres, string word) => storage.GetFilePath(addres, word);
}