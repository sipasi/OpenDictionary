#nullable enable

using System.Threading.Tasks;

namespace OpenDictionary.Services.Audio;

public interface IPhoneticFilesService
{
    Task<bool> AddFromWebAsync(string word, string address);
    bool Contains(string word, string address);
    bool Any(string word);
    string? GetFilePath(string word, string address);
}