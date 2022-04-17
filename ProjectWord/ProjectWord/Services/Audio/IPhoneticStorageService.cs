#nullable enable

using System.Threading.Tasks;

namespace OpenDictionary.Services.Audio
{
    public interface IPhoneticStorageService
    {
        ValueTask<bool> AddFromWebAsync(string address, string name);
        bool Contains(string name);
        string GetFilePath(string name);
    }
}