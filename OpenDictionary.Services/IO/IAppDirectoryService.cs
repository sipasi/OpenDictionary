#nullable enable

namespace OpenDictionary.Services.IO
{
    public interface IAppDirectoryService
    {
        public string AppData { get; }
        public string Cache { get; }
    }
}