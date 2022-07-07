#nullable enable

using System.Threading.Tasks;

namespace OpenDictionary.Services.Audio;

public interface IPhoneticFilesService
{
    Task<bool> AddFromWebAsync(string address, string word);
    bool Contains(string address, string word);
    bool Any(string word);
    string? GetFilePath(string address, string word);
}